
function faIconPropertyEditorController($scope, editorService, $sce) {
    var vm = this;

    vm.add = add;
    vm.showPrompt = showPrompt;
    vm.hidePrompt = hidePrompt;
    vm.remove = remove;

    // FaLink Row Functions
    function add() {
        var item = {
            "className": "",
            "svg": "",
            "label": ""
        };
        $scope.model.value.push(item);
    }

    function showPrompt(item) {
        item.deletePrompt = true;
    }

    function hidePrompt(item) {
        item.deletePrompt = false;
    }

    function remove($index, item) {
        $scope.model.value.splice($index, 1);
    }

    $scope.sortableOptions = {
        distance: 10,
        tolerance: 'pointer',
        opacity: 0.7,
        scroll: true,
        cursor: 'move',
        handle: ".list-view-layout__sort-handle"
    };

    // Icon Picker
    if (!$scope.model.value) {
        $scope.model.value = [];
        vm.add();
    }

    $scope.addIcon = function (item) {
        var faPicker = {
            title: "Font Awesome Icon Search",
            view: "/App_Plugins/FaLinksPropertyEditor/faLinks.picker.html",
            size: "small",
            submit: function (model) {
                item.svg = model.svg;
                item.className = model.className;
                item.label = model.label;
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        };
        editorService.open(faPicker);
    };

    $scope.removeIcon = function(item) {
        item.svg = "";
        item.className = "";
        item.label = "";
    };

    // Render svg
    $scope.trustAsHtml = $sce.trustAsHtml;

}
angular.module('umbraco').controller("FaIcon.PropertyEditor.Controller", faIconPropertyEditorController);
