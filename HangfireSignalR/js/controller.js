var app = angular.module('app');

app.controller('TaskListCtrl', function ($scope, Hub) {

	var hub = new Hub('tasksHub', {
		listeners: {
			'taskStarted': function (taskId) {
					console.log(taskId + ': started');
			},
			'progressChanged': function (taskId, progress) {
					console.log(taskId + ': ' + progress + '%');
			},
			'taskCompleted': function (taskId) {
					console.log(taskId + ': completed');
			}
		},

		methods: ['getAllTasks', 'startTask', 'cancelTask'],

		stateChanged: function(state) {
			switch(state) {
				case $.signalR.connectionState.connected:
					hub.getAllTasks().done(function(tasks) {
						console.dir(tasks);
					});
					break;
			}
		},

		logging: true
	});

	$scope.tasks = [];

	$scope.newTask = {};

	$scope.startTask = function (task) {
		if (task.name && task.delay) {
			hub.startTask(task.name, task.delay * 1000);
		}
	};
});