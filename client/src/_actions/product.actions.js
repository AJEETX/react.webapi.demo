import { productConstants } from '../_constants';
import { productService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const productActions = {
    getProducts,
    add:addproduct,
    getById:getProductById,
    update:updateProduct,
    delete: _delete
};

function getProducts() {
    return dispatch => {
        dispatch(request());

        productService.getProducts()
            .then(
                products => dispatch(success(products)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: productConstants.GETALL_REQUEST } }
    function success(products) { return { type: productConstants.GETALL_SUCCESS, products } }
    function failure(error) { return { type: productConstants.GETALL_FAILURE, error } }
}
function addproduct(product) {
    return dispatch => {
        dispatch(request(product));

        productService.addProduct(product)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/');
                    dispatch(alertActions.success('Added successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(product) { return { type: productConstants.ADD_REQUEST, product } }
    function success(product) { return { type: productConstants.ADD_SUCCESS, product } }
    function failure(error) { return { type: productConstants.ADD_FAILURE, error } }
}
function getProductById(id) {
    return dispatch => {
        dispatch(request(id));

        productService.getProductById(id)
            .then(
                (product) => { 
                    dispatch(success(product));
                    history.push('/update')
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: productConstants.GET_PRODUCT_REQUEST, id } }
    function success(product) { return { type: productConstants.GET_PRODUCT_SUCCESS, product } }
    function failure(id, error) { return { type: productConstants.GET_PRODUCT_FAILURE, id, error } }
}
function updateProduct(product) {
    return dispatch => {
        dispatch(request(product));

        productService.updateProduct(product)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/');
                    dispatch(alertActions.success('Update successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(product) { return { type: productConstants.UPDATE_REQUEST, product } }
    function success(product) { return { type: productConstants.UPDATE_SUCCESS, product } }
    function failure(error) { return { type: productConstants.UPDATE_FAILURE, error } }
}
// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        productService.delete(id)
            .then(
                () => { 
                    dispatch(success(id));
                    dispatch(alertActions.success('Delete successful'));
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: productConstants.DELETE_REQUEST, id } }
    function success(id) { return { type: productConstants.DELETE_SUCCESS, id } }
    function failure(id, error) { return { type: productConstants.DELETE_FAILURE, id, error } }
}