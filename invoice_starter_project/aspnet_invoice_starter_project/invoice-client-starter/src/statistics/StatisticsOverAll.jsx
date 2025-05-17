

import React, {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {apiGet} from "../utils/api";



const Statistics = () => {
    const {id} = useParams();
    const [statistics, setStatistics] = useState({});
 
    useEffect(() => {
        apiGet("/api/Invoices/Statistics")
        .then((data)=>{
            setStatistics(data)
        })
        .catch ((error)=> {
            console.error(error);
        });
    }, []);


        return (
            <div className="d-flex justify-content-center align-items-center" >
      <div className="d-flex flex-wrap gap-4">
                <div className="card p-3 shadow-sm" style={{ minWidth: '200px' }}>
              <h6 className="card-title text-muted">Součet za letošní rok:</h6>
              <p className="card-text display-6">{statistics.currentYearSum?.toLocaleString('cs-CZ')} Kč</p>
            </div>
            <div className="card p-3 shadow-sm" style={{ minWidth: '200px' }}>
              <h6 className="card-title text-muted">Součet celkem:</h6>
              <p className="card-text display-6">{statistics.allTimeSum?.toLocaleString('cs-CZ')} Kč</p>
            </div>
            <div className="card p-3 shadow-sm" style={{ minWidth: '200px' }}>
              <h6 className="card-title text-muted">Počet faktur:</h6>
              <p className="card-text display-6">{statistics.invoicesCount}</p>
            </div>
          </div>
          </div>
          );
          

      
};
  
  export default Statistics;