

import React, {useEffect, useState} from "react";
import {apiGet, apiDelete} from "../utils/api";
import InvoiceTable from "../invoices/InvoiceTable";

const PersonStatistics = (props) => {
        const [invoices,setInvoices] = useState([]);
      console.log("personstatistics - idNumber: "+props.identificationNumber+", type:"+props.type)
   
     useEffect(() => {
        console.log("provedl se useEffect na IČO");
         if (props.identificationNumber) {
             apiGet("/api/identification/"+props.identificationNumber+"/"+props.type)
            .then((data)=>{
                setInvoices(data)
            })
            .catch ((error)=> {
                console.error(error);
            });
        }
        else {
            console.log("IČO ještě není načteno");
        }
    }, [props.identificationNumber]);
 
       const deleteInvoice = async (id) => {
            try {
                await apiDelete("/api/Invoices/" + id);
            } catch (error) {
                console.log(error.message);
                alert(error.message)
            }
            setInvoices(invoices.filter((item) => item._id !== id));
             alert("Smazána faktura s id: "+id);
        };

    return (
        <>
         <InvoiceTable
                deleteInvoice={deleteInvoice}
                items={invoices}
                label="Počet faktur:"
            />

          
        </>
    );
};

export default PersonStatistics;
