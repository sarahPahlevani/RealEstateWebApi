export default function({ store, redirect, route, params }) {
  console.log(route)
  if (store.state.token) {
    if (params.lang) return redirect(`/${params.lang}/`)
    else return redirect('/')
  }
}
