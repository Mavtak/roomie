import commonApi from '../../../api.js';

function api(
  $q
) {
  return function (request) {
    let result = commonApi(request);
    let wrappedResult = $q.when(result);

    return wrappedResult;
  }
}

export default api;
