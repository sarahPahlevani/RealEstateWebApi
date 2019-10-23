export default {
  install(Vue) {
    Vue.toasted.register(
      'error',
      (message) => {
        if (!message || typeof message !== 'string') {
          return 'Oops.. Something Went Wrong..'
        }
        return message
      },
      {
        position: 'top-center',
        type: 'error',
        duration: 8000,
        className: 'animated bounceIn fast'
      }
    )

    Vue.toasted.register(
      'warn',
      (message) => {
        if (!message || typeof message !== 'string') {
          return 'Warning!!!'
        }
        return message
      },
      {
        position: 'bottom-center',
        type: 'info',
        duration: 5000,
        className: 'animated bounceIn bg-warning text-dark'
      }
    )

    Vue.toasted.register(
      'deleted',
      (message) => {
        if (!message || typeof message !== 'string') {
          return 'Item Deleted'
        }
        return message
      },
      {
        position: 'bottom-right',
        type: 'success',
        duration: 5000,
        className: 'animated bounceIn'
      }
    )
  }
}
