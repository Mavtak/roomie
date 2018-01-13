import Headers from './globals/Headers.js';
import fetch from './globals/fetch.js';
import JSON from './globals/JSON.js';

function jsonFetch({
  action,
  parameters,
  repository,
}) {
  let path = `/api/${repository}`;
  let options = {
    credentials: 'include',
    body: JSON.stringify({
      action: action,
      parameters: parameters,
    }),
    headers: new Headers({
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    }),
    method: 'POST',    
  };
  
  return fetch(path, options)
    .then(response => {
      return response.json();
    });
};

export default jsonFetch;
