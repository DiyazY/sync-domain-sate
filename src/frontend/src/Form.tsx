import React, { useEffect } from "react";
import { useQueryClient } from "react-query";
import Entity from "./Entity";
import { getSignalRService } from "./signalR";
import Updater from "./Updater";

let hub: any = undefined;

function Form() {
  const queryClient = useQueryClient();

  useEffect(() => {
    hub = getSignalRService();
    hub.on("updated-entity", (entityId: any) => {// subscribes on given method
      console.log(entityId);
      queryClient.invalidateQueries("entity_" + entityId); // pushes react query to refecth given resource
    });
    return () => {
      hub.off("updated-entity");
    };
  }, [queryClient]);
  
  return (
    <>
      <Entity />
      <Updater></Updater>
    </>
  );
}

export default Form;
