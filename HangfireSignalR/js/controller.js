var app = angular.module('app', []);

app.controller('TaskListCtrl', function ($scope) {
	$scope.tasks = [
    {
    	name: 'Nexus S',
    	progress: 5
    },
    {
    	name: 'bbbbb',
    	progress: 75
    },
    {
    	name: 'cccccc',
    	progress: 35
    }
	];
});