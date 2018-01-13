function received() {

  return function (task) {
    if (task.receivedTimestamp) {
      return task.receivedTimestamp.toLocaleString();
    }

    if (task.expired) {
      return 'Expired';
    }

    return '';
  };

}

export default received;
