var app = angular.module('app');

app.constant('Enums', {
	taskState: {
		completed: 0,
		running: 1,
		canceled: 2,
		failed: 3
	}
});

app.controller('TaskListCtrl', function ($rootScope, $scope, Hub, Enums) {

	var findById = function(id) {
		return $scope.tasks.find(function (t) { return t.id == id; });
	};

	var hub = new Hub('tasksHub', {
		listeners: {
			'taskStarted': function (task) {
				//console.log(taskId + ': started');
				$scope.tasks.push(task);
				$rootScope.$apply();  //http://stackoverflow.com/questions/21658490/angular-websocket-and-rootscope-apply
			},
			'progressChanged': function (taskId, progress) {
				//console.log(taskId + ': ' + progress + '%');
				var task = findById(taskId);
				if (task) {
					task.progress = progress;
					$rootScope.$apply();
				};
			},
			'taskStateUpdated': function (taskId, state) {
				//console.log(taskId + ': completed');
				var task = findById(taskId);
				if (task) {
					task.state = state;
					$rootScope.$apply();
				}
			}
		},

		methods: ['getAllTasks', 'startTask', 'cancelTask'],

		stateChanged: function (state) {
			switch (state.newState) {
				case $.signalR.connectionState.connected:
					hub.getAllTasks().done(function (tasks) {
						$scope.tasks = tasks;
						$rootScope.$apply();
					});
					break;
			}
		},

		logging: true
	});

	$scope.taskStateEnum = Enums.taskState;

	$scope.tasks = [];

	$scope.newTask = {};

	$scope.resetNewTask = function () {
		$scope.newTask = {
			name: '',
			delay: 3,
			failAfter: 0
		};
	};

	$scope.startTask = function (task) {
		if (task.name && task.delay) {
			hub.startTask(task.name, task.delay * 1000, task.failAfter);
			$scope.resetNewTask();
		}
	};

	$scope.cancel = function(task) {
		hub.cancelTask(task.id);
	};

	$scope.resetNewTask();
});