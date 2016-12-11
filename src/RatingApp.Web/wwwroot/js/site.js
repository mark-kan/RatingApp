
var app = angular.module('ratingApp', ['ngSanitize', 'ui.bootstrap', 'ui.select'])

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common = {
        'X-XSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
    };
}]);
app.controller('ratingCtrl', function ($scope, $http) {

    $scope.itemArray = [];
 
    $scope.search = function (val) {
    
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
         
        $http.get('/Skills/UserSkills')
            .then(function (response) {
                $scope.userSkills = response.data;
            }, function error(response) {
                alert("Unable to add skill");
            });
    }

    $scope.onSelectUserSkillLevel = function (userSkill, level) {
        
        $http.post('/Skills/UserSkillLevel', {
            'SkillId': userSkill.skill.skillId,
            'Level': level,
           
           
        }).then (function(response) {
            userSkill.level = level;
        });
    }

    $scope.onDeleteUserSkill = function (userSkill) {
      
        $http({
            method: 'DELETE',
            url: '/Skills/UserSkills',
            data: {SkillId: userSkill.skill.skillId},
            headers: { 'Content-type': 'application/json' }
          
        }).then (function (response) {
            $scope.getUserSkills();
        }, function(rejection){
            alert('Unable to remove skill');   
        });
    }
    
});