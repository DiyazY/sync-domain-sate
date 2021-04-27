import React from "react";

function Updater() {
    
    const updateEntity = () => {
        let id = (document.getElementById("enity_id") as HTMLInputElement).value ;
        fetch(`http://localhost:5031/trigger-update-entity?id=${id}`);
    };

    return (
        <>
            <input type="text" placeholder="type enity id" id="enity_id"></input>
            <button onClick={updateEntity}>update</button>
        </>
    );
}

export default Updater;
