// Write your Javascript code.


var app = angular.module('ratingApp', ['ngSanitize', 'ui.bootstrap', 'ui.select']);



app.controller('ratingCtrl', function ($scope, $http) {

    $scope.itemArray = [];
    //$scope.skill = $scope.result[0];

    $scope.search = function (val) {
        console.log(val);
    
      if (val.length < 1) {
          $scope.itemArray = undefined;
           return;
       }

        $http.post('/Skills/Search/', {
            'SearchTerm' : val.trim()
         })
        .then(function (response) {
            $scope.itemArray = response.data;
        });

 //       $scope.selected = { value: $scope.itemArray[0] };
 
         
    };

    $scope.onSkillSelect = function (skill) {

        if (skill != null) {

            var skillId = skill.skillId;

            $http.post('/Skills/Add/', {
                'SkillId': skillId,
                'SkillName': skill.skillName
            }).then(function (response) {
                skill.skillName = "";
                $scope.getUserSkills();
          
            });
        }
    };

    $scope.getUserSkills = function () {
        console.log("content loaded");
         
        $http.get('/Skills/UserSkills')
            .then(function (response) {
                $scope.userSkills = response.data;
            }, function error(response) {
                alert("Unable to add skill");
                console.log("Error");
            });
    }

    $scope.onSelectUserSkillLevel = function (userSkill, level) {
        $http.post('/Skills/UserSkillLevel', {
            'SkillId': userSkill.skill.skillId,
            'Level': level
        }).then (function(response) {
            console.log("Skill level updated");
            userSkill.level = level;
        });
    }

    $scope.onDeleteUserSkill = function (userSkill) {

        console.log("Delete skill: " + userSkill.skill.skillId);
        $http({
            method: 'DELETE',
            url: '/Skills/UserSkills',
            data: {SkillId: userSkill.skill.skillId}
            ,
            headers: { 'Content-type': 'application/json' }
        }).then (function (response) {
            console.log(response.data);
            $scope.getUserSkills();
        }, function(rejection){
            alert('Unable to remove skill');   
        });
    }
    
});