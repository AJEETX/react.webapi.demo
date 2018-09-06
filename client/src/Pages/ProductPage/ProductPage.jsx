import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { productActions } from '../../_actions';

class ProductPage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            product: {
                Description: '',
                Brand: '',
                Model: ''
            },
            submitted: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
  
    handleChange(event) {
        const { name, value } = event.target;
        const { product } = this.state;
        this.setState({
            product: {
                ...product,
                [name]: value
            }
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.setState({ submitted: true });
        const { product } = this.state;
        const { dispatch } = this.props;
        if (product.Description && product.Brand && product.Model) {
            dispatch(productActions.add(product));
        }
    }

    render() {
        const { product, submitted } = this.state;
        return (
            <div className="col-md-6 col-md-offset-3">
                               <p> <Link to="/login">Logout</Link></p>
                <h2>Add product</h2>
                <form name="form" onSubmit={this.handleSubmit}>
                    <div className={'form-group' + (submitted && !product.Description ? ' has-error' : '')}>
                        <label htmlFor="Description">Description</label>
                        <input type="text" className="form-control" name="Description" value={product.Description} onChange={this.handleChange} />
                        {submitted && !product.Description &&
                            <div className="help-block">Description is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !product.Brand ? ' has-error' : '')}>
                        <label htmlFor="Brand">Brand</label>
                        <input type="text" className="form-control" name="Brand" value={product.Brand} onChange={this.handleChange} />
                        {submitted && !product.Brand &&
                            <div className="help-block">Brand is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !product.Model ? ' has-error' : '')}>
                        <label htmlFor="Model">Model</label>
                        <input type="text" className="form-control" name="Model" value={product.Model} onChange={this.handleChange} />
                        {submitted && !product.Model &&
                            <div className="help-block">Model is required</div>
                        }
                    </div>
                    <div className="form-group">
                        <button className="btn btn-primary">Add product</button>
                        <Link to="/" className="btn btn-link">Cancel</Link>
                    </div>
                </form>

            </div>
        );
    }
}

function mapStateToProps(state) {
    const { products } = state.products;
    return {
        products
    };
}

const connectedProductPage = connect(mapStateToProps)(ProductPage);
export { connectedProductPage as ProductPage };