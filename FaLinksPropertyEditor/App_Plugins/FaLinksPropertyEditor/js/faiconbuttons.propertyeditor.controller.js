﻿
function faIconButtonsPropertyEditorController($scope, $sce) {
   var vm = this;
   vm.showLabel = false;
   vm.allowMultiple = false; 
   vm.buttons = [];

    vm.setClasses = function (value) {
        var index = $scope.model.value.indexOf(value);

        let classes = {
            'btn-selection': index !== -1,
            'btn-info': index === -1,
            'btn-wide': vm.showLabel
        };
        return classes;
    };

    vm.add = function (value) {
 
        var index = $scope.model.value.indexOf(value);

        if (vm.allowMultiple) {

            if (index !== -1) {
                $scope.model.value.splice(index, 1);

            } else {
                $scope.model.value.push(value);
            }
        } else {    

            $scope.model.value = [];

            if (index === -1) {
                $scope.model.value.push(value);
            }
        }
    };

    $scope.trustAsHtml = $sce.trustAsHtml;

    vm.init = function () {
        if ($scope.model.config && $scope.model.config.faIconButtons){
            vm.buttons = $scope.model.config.faIconButtons;
        }
        if ($scope.model.config && $scope.model.config.showLabel) {
            vm.showLabel = $scope.model.config.showLabel !== '0';
        }
        if ($scope.model.config && $scope.model.config.allowMultiple) {
            vm.allowMultiple = $scope.model.config.allowMultiple !== '0';
        }
    };

    if (!$scope.model.value) {
        $scope.model.value = [];
    }

    vm.init();
}
angular.module('umbraco').controller("FaIconButtons.PropertyEditor.Controller", faIconButtonsPropertyEditorController);
