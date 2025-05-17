import React from "react";
import InputSelect from "../components/InputSelect";
import InputField from "../components/InputField";

const InvoiceFilter = (props) => {

    const handleChange = (e) => {
        props.handleChange(e);
    };

    const handleSubmit = (e) => {
        props.handleSubmit(e);
    };

    const filter = props.filter;

    return (
        <form onSubmit={handleSubmit}>
                <div>
                    {/* <div className="col">
                        <InputSelect
                            name="buyerID"
                            items={props.personsList}
                            handleChange={handleChange}
                            label="Odběratel"
                            prompt="nevybrán"
                            value={filter.personsID}
                        />
                    </div>
                    <div className="col">
                        <InputSelect
                            name="sellerID"
                            items={props.personsList}
                            handleChange={handleChange}
                            label="Dodavatel"
                            prompt="nevybrán"
                            value={filter.personsID}
                        />
                    </div> */}
                    <div className="col">
                        <InputField
                            type="text"
                            min="0"
                            name="product"
                            handleChange={handleChange}
                            label="Název"
                            prompt="neuveden"
                            value={filter.product ? filter.product : ''}
                        />
                    </div>
                    <div className="col">
                        <InputField
                            type="number"
                            min="1"
                            name="minPrice"
                            handleChange={handleChange}
                            label="Minimální cena"
                            prompt="neuvedena"
                            value={filter.minPrice ? filter.minPrice: ''}
                        />
                    </div>
                    <div className="col">
                        <InputField
                            type="number"
                            min="1"
                            name="limit"
                            handleChange={handleChange}
                            label="Limit počtu faktur"
                            prompt="neuveden"
                            value={filter.limit ? filter.limit : ''}
                        />
                    </div>
                

                
                    <div className="col">
                        <input
                            type="submit"
                            className="btn btn-secondary float-right mt-2"
                            value={props.confirm}
                        />
                    </div>
                </div>
        </form>
    );

};

export default InvoiceFilter;