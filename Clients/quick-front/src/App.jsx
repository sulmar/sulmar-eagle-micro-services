import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import axios from "axios";

function App() {
  const [count, setCount] = useState(0)
  const [customers, setCustomers] = useState([]);
  const sendRequest = async () => {
    try{
      const response = await axios.get("http://localhost:5263/api/customers", {headers: {"x-secret-key": "abc"}})
      const customers = response.data;
      setCustomers(customers);
      console.log(customers);
    }
    catch(err){
      console.error("Error occurred");
    }
  }
  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src="/vite.svg" className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => sendRequest()}>
          Fetch Customers
        </button>
        {customers.map(c => <div>{c?.name}</div>)}
        <p>
          Edit <code>src/App.jsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </div>
  )
}

export default App
