import React, { useEffect, useState } from "react";
import "./App.css";
import { initSignalRService } from "./signalR";
import { QueryClient, QueryClientProvider } from "react-query";
import Form from "./Form";

const queryClient = new QueryClient();

function App() {
  const [isConnected, setIsConnected] = useState(false);
  useEffect(() => {
    initSignalRService().then((isConnected) => setIsConnected(isConnected));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (!isConnected) return <p>Connecting...</p>;

  return (
    <QueryClientProvider client={queryClient}>
      <div className="App">
        <header className="App-header">
          <Form/>
        </header>
      </div>
    </QueryClientProvider>
  );
}

export default App;
