export default ({ store, app: { $axios } }) => {
  $axios.setToken(store.getters.getToken)
}
