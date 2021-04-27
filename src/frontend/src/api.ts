function updateEntity(id: string) {
  fetch(`http://localhost:5031/trigger-update-entity?id=${id}`);
}

function getEntity(id: string) {
  return fetch(`http://localhost:5031/get-entity?id=${id}`).then((res) => res.text());
}

export { updateEntity,getEntity };
