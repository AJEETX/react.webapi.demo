import { authHeader, config } from '../_helpers';

export const productService = {
    addProduct,
    getProducts,
    getProductById,
    updateProduct,
    delete: _delete
};
function addProduct(product) {
    const requestOptions = {
        method: 'POST',
        headers:  { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(product)
    };

    return fetch(config.apiUrl + '/products', requestOptions).then(handleResponse, handleError);
}
function getProducts() {
    const requestOptions = {
        method: 'GET',
        headers:  { ...authHeader(), 'Content-Type': 'application/json' },
    };

    return fetch(config.apiUrl + '/products', requestOptions).then(handleResponse, handleError);
}

function getProductById(id) {
    const requestOptions = {
        method: 'GET',
        headers:  { ...authHeader(), 'Content-Type': 'application/json' },
    };

    return fetch(config.apiUrl + '/products/' + id, requestOptions).then(handleResponse, handleError);
}


function updateProduct(product) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(product)
    };

    return fetch(config.apiUrl + '/products/' + product.id, requestOptions).then(handleResponse, handleError);
}

function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/products/' + id, requestOptions).then(handleResponse, handleError);
}

function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            response.text().then(text => reject(text));
        }
    });
}

function handleError(error) {
    return Promise.reject(error && error.message);
}