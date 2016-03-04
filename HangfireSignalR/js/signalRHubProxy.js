// http://henriquat.re/server-integration/signalr/integrateWithSignalRHubs.html

'use strict';

var app = angular.module('app');

app.factory('signalRHubProxy', function ($rootScope) {
	function signalRHubProxyFactory(hubName, startOptions, onConnectionEstablished) {
		var connection = $.hubConnection();
		var proxy = connection.createHubProxy(hubName);
		connection.start(startOptions).done(onConnectionEstablished || function () { });

		return {
			on: function (eventName, callback) {
				proxy.on(eventName, function (result) {
					$rootScope.$apply(function () {
						if (callback) {
							callback(result);
						}
					});
				});
			},
			off: function (eventName, callback) {
				proxy.off(eventName, function (result) {
					$rootScope.$apply(function () {
						if (callback) {
							callback(result);
						}
					});
				});
			},
			invoke: function () {
				var args = Array.prototype.slice.call(arguments);
				var callback = args[args.length - 1];
				if (callback instanceof Function) {
					args = args.slice(0, args.length - 1);
				} else {
					callback = null;
				}

				proxy.invoke.apply(this, args)
					.done(function (result) {
						$rootScope.$apply(function () {
							if (callback) {
								callback(result);
							}
						});
					});
			},

			invoke2: function() {
				proxy.invoke('getAllTasks');
			},

			connection: connection
		};
	};

	return signalRHubProxyFactory;
});