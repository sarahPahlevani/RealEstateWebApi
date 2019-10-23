export default function({ store, redirect, route, params }) {
  if (!store.state.token) {
    if (params.lang)
      return redirect(`/${params.lang}/login?redirect=` + route.fullPath)
    else return redirect('/login?redirect=' + route.fullPath)
  }
}
