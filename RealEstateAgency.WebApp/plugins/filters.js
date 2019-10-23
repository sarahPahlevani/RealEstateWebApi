export default {
  install(Vue) {
    Vue.filter('currency', function(value) {
      const price = parseFloat(value).toFixed(2)
      const currency = window.currency || '$'
      return `${currency}${price}`
    })

    Vue.filter('sub', function(value, length = 10) {
      if (value.length <= length) return value
      return `${value.substr(0, length)} ...`
    })

    Vue.filter('upperChar', function(str, length = 1) {
      if (typeof str !== 'string') {
        str = String(str)
      }

      str = str.trim()
      return str.substr(0, length).toUpperCase() + str.substr(length)
    })

    Vue.filter('upperCase', function(str) {
      if (typeof str !== 'string') {
        str = String(str)
      }
      str = str.trim()
      return str.toUpperCase()
    })
    Vue.filter('highlight', function(words, query) {
      if (!query) return words
      query = query.toLowerCase()
      words = words.toLowerCase()
      return words.replace(
        query,
        '<span class="highlighted">' + query + '</span>'
      )
    })
  }
}
