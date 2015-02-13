var module = angular.module('roomie.tasks');

module.directive('taskWidget', function () {
  return {
    restrict: 'E',
    scope: {
      task: '=task'
    },
    template: '' +
      '<widget>' +
        '<widget-header title="Task"></widget-header>' +
        '<widget-data-section>' +
          '<key-value key="Origin" value="{{task.origin}}"></key-value>' +
          '<key-value key="Created" value="{{task.script.creationTimestamp.toLocaleString()}}"></key-value>' +
          '<key-value key="Target" value="{{task.target.name}}" href="/computer/{{task.target.id}}/{{task.target.name}}"></key-value>' +
          '<key-value key="Received" value="{{task | received}}"></key-value>' +
        '</widget-data-section>' +
        '<textarea class="code" readonly>{{task.script.text}}</textarea>' +
      '</widget>'
  };
});
