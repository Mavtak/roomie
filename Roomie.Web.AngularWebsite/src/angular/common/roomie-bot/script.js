function roomieBot() {

  return {
    restrict: 'E',
    scope: {
      mood: '=mood',
    },
    templateUrl: 'common/roomie-bot/template.html',
  };

}

export default roomieBot;
