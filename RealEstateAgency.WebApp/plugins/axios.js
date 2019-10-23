export default function({ store, app: { $axios }, redirect }) {
  $axios.onRequest((config) => {
    console.log('Making request to ' + config.url)
  })

  $axios.onError((error) => {
    const code = parseInt(error.response && error.response.status)
    console.log(code, error.response)
    // if (code === 404) {
    //   redirect('/404')
    // }
  })
}
