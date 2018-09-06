import { productConstants } from '../_constants';
const initialState={
  items:[],
  loading:false,
  error:null,
  item:{}
}
export function products(state = initialState, action) {
  switch (action.type) {
    case productConstants.GETALL_REQUEST:
      return {
        ...state,
        loading: true,
        error: null
      };
    case productConstants.GETALL_SUCCESS:
      return {
        ...state,
        loading: false,
        items: action.products
      };
    case productConstants.GETALL_FAILURE:
      return { 
        ...state,
        loading: false,
        error: action.error,
        items: []
      };
      case productConstants.ADD_REQUEST:
      return {
        ...state,
        loading: true,
        error: null
      };
    case productConstants.ADD_SUCCESS:
      return {
        ...state,
        loading: false,
        error: null
      };
    case productConstants.ADD_FAILURE:
      return { 
        ...state,
        loading: false,
        error: action.error,
        items: []
      };

      case productConstants.GET_PRODUCT_REQUEST:
      return {
        ...state,
        loading: true,
        error: null
      };
    case productConstants.GET_PRODUCT_SUCCESS:
    {
      return {
        ...state,
        loading: false,
        error: null,
        item:action.product
      };
    }
    case productConstants.GET_PRODUCT_FAILURE:
      return { 
        error: action.error
      };

    case productConstants.DELETE_REQUEST:

      return {
        ...state,
        items: state.items.map(product =>
          product.id === action.id
            ? { ...product, deleting: true }
            : product
        )
      };
    case productConstants.DELETE_SUCCESS:

      return {
        items: state.items.filter(product => product.id !== action.id)
      };
    case productConstants.DELETE_FAILURE:

      return {
        ...state,
        items: state.items.map(product => {
          if (product.id === action.id) {

            const { deleting, ...productCopy } = product;

            return { ...productCopy, deleteError: action.error };
          }

          return product;
        })
      };
    default:
      return state
  }
}