angular.module('roomie.common').factory('pageMenuItems', function () {

  return new PageMenuItems();

  function PageMenuItems() {
    var _items = [];

    this.any = function() {
      return _items.length > 0;
    };

    this.list = function() {
      return _items;
    };

    this.reset = function() {
      this.set([]);
    };

    this.set = function(items) {
      if (!_.isArray(items)) {
        throw new Error('items must be array');
      }

      _items = items;
    };
  }

});
