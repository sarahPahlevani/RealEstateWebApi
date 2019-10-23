export default {
  install(Vue) {
    Vue.prototype.$handleError = (err, after) => {
      if (err.status === 500) {
        let message = 'Please try again later!'
        if (err && typeof err === 'string') message = err
        Vue.prototype.$bus.$emit('internal-server', message)
        console.log(err)
        if (after) after()
        return
      }
      if (err.status === 400) {
        let message = 'Bad request!'
        if (err && err.Message) message = err.Message
        Vue.prototype.$bus.$emit('bad-request', message)
        console.log(err)
        if (after) after()
        return
      }
      if (err.status === 403) {
        let message = 'Access denied!'
        if (err && typeof err === 'string') message = err
        Vue.prototype.$bus.$emit('access-denied', message)
        console.log(err)
        if (after) after()
      }
    }

    Vue.prototype.$logError = (err, after) => {
      if (err.status === 500) {
        let message = 'Please try again later!'
        if (err && typeof err === 'string') message = err
        console.log(message, err)
        if (after) after()
        return
      }
      if (err.status === 400) {
        let message = 'Bad request!'
        if (err && err.Message) message = err.Message
        console.log(message, err)
        if (after) after()
        return
      }
      if (err.status === 403) {
        let message = 'Access denied!'
        if (err && typeof err === 'string') message = err
        console.log(message, err)
        if (after) after()
      }
    }
  }
}
