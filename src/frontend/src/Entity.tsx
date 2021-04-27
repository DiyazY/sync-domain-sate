import React from "react";
import { useQuery } from "react-query";
import { getEntity } from "./api";

function Entity() {
  
  let id = "1";

  // Queries
  const { isLoading, data, isFetching } = useQuery("entity_"+id, () =>
    getEntity(id)
  );

  if (isLoading) return <>Loading...</>;
  if (isFetching) return <>Fetching...</>;
  return <>{data}</>;
}

export default Entity;
