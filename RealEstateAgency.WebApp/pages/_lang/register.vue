<template>
  <section class="sptb">
    <div class="container customerpage">
      <div class="row">
        <div class="col-lg-5 col-xl-4 col-md-6 d-block mx-auto">
          <div class="row">
            <div class="col-xl-12 col-md-12 col-md-12 register-right">
              <div class="tab-content" id="myTabContent">
                <div
                  class="tab-pane fade show active"
                  id="home"
                  role="tabpanel"
                  aria-labelledby="home-tab"
                >
                  <div class="single-page  w-100  p-0">
                    <div class="wrapper wrapper2">
                      <type active-link="Register"></type>
                      <register></register>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import Register from '~/components/Auth/Register'
import Type from '~/components/Auth/Type'
export default {
  middleware: 'notAuthenticated',
  components: {
    Register,
    Type
  }
}
</script>

<style>
.title {
  font-family: 'Quicksand', 'Source Sans Pro', -apple-system, BlinkMacSystemFont,
    'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
  display: block;
  font-weight: 300;
  font-size: 100px;
  color: #35495e;
  letter-spacing: 1px;
}
</style>
