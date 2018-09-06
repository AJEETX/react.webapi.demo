import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { productActions } from '../../_actions';
class HomePage extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
         this.props.dispatch(productActions.getProducts());       
    }
    handleEditProduct(id) {
        this.props.dispatch(productActions.getById(id)); 
    }

    handleDeleteProduct(id) {
        return (e) => this.props.dispatch(productActions.delete(id));
    }
    render() {
        const { user,products } = this.props;
        return (
            <div className="col-md-6 col-md-offset-3">
                   <p> <Link to="/login">Logout</Link></p>
                <h4>Welcome  {user.firstName} {user.lastName} !</h4>
                <Link to="/product" className="btn btn-link">Add product</Link>
                <div>
                {products.items &&
                <table className="table table-striped">
                    <thead>
                    <tr>
                            <th>Description</th>
                            <th>brand</th>
                            <th>model</th>
                            <th>edit</th>
                            <th>delete</th>
                        </tr>
                    </thead>
                    <tbody>
                    {products.items.map((product, index) =>
                    <tr key={product.id}>
                    <td>{product.description}</td>
                    <td>{product.brand}</td>
                    <td>{product.model}</td>
                    <td>{
                                 product.editing ? <em> - Editing...</em>
                                 : product.editError ? <span className="text-danger"> - ERROR: {product.editeError}</span>
                                 :<span onClick={this.handleEditProduct.bind(this,product.id)} className="glyphicon glyphicon-pencil"> </span>
                        }
                    </td>
                    <td>{
                                 product.deleting ? <em> - Deleting...</em>
                                 : product.deleteError ? <span className="text-danger"> - ERROR: {product.deleteError}</span>
                                 :<span onClick={this.handleDeleteProduct(product.id)} className="glyphicon glyphicon-remove"> </span>
                        }
                    </td>
                    </tr>
                    )}
                    </tbody>
                </table>
                }
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const {  authentication,products } = state;
    const { user} = authentication;
    return {
        user,
        products
    };
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };