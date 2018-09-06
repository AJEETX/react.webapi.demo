import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { productActions } from '../../_actions';

class UpdatePage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            submitted: false
        };
    }
    onChange(e) {
        this.setState({[e.target.name]: e.target.value})
    }

    handleSubmit(event) {
        event.preventDefault();
        this.setState({ 
            submitted: true 
        });

        let id=this.props.product.id
        let product={
                id:id,
                description:this.refs.description.value,
                brand:this.refs.brand.value,
                model:this.refs.model.value
        }
        if ( product.description && product.brand && product.model) {
            this.props.dispatch(productActions.update(product));
        }
    }

    render() {
        const { product } = this.props;
        const {description,brand,model,submitted}=this.state
        return (
            <div className="col-md-6 col-md-offset-3">
                               <p> <Link to="/login">Logout</Link></p>
                <h4>Update product</h4>
                <form name="form" onSubmit={this.handleSubmit.bind(this)}>
                    <div className={'form-group' + ( submitted && !description ? ' has-error' : '')}>
                        <label htmlFor="Description">Description</label>
                        <input type="text" className="form-control" name="Description" value={description} defaultValue={product.description} 
                        onChange={(value) => this.onChange(value)} ref="description" />
                        {submitted && !description &&
                            <div className="help-block">Description is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !brand ? ' has-error' : '')}>
                        <label htmlFor="Brand">Brand</label>
                        <input type="text" className="form-control" name="Brand" value={brand} defaultValue={product.brand} 
                        onChange={(value) => this.onChange(value)} ref="brand" />
                        { submitted && !brand &&
                            <div className="help-block">Brand is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !model ? ' has-error' : '')}>
                        <label htmlFor="Model">Model</label>
                        <input type="text" className="form-control" name="Model" value={model} defaultValue={product.model} 
                        onChange={(value) => this.onChange(value)} ref="model"  />
                        { submitted &&!model &&
                            <div className="help-block">Model is required</div>
                        }
                    </div>
                    <div className="form-group">
                        <button className="btn btn-primary">Update product</button>
                        <Link to="/" className="btn btn-link">Cancel</Link>
                    </div>
                </form>

            </div>
        );
    }
}

function mapStateToProps(state) {
    const { products } = state;
    return {
        product:products.item,
    };
}

const connectedUpdatePage = connect(mapStateToProps)(UpdatePage);
export { connectedUpdatePage as UpdatePage };