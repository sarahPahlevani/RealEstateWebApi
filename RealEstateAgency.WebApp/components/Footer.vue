<template>
  <footer class="bg-dark-purple text-white">
    <div class="footer-main">
      <div class="container">
        <div class="row">
          <div class="col-lg-3 col-md-12">
            <h6>About</h6>
            <hr
              class="deep-purple  accent-2 mb-4 mt-0 d-inline-block mx-auto"
            />
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis exercitation ullamco laboris
            </p>
            <p>
              Duis aute irure dolor in reprehenderit in voluptate velit esse
              cillum.
            </p>
          </div>
          <div class="col-lg-2 col-md-12">
            <h6>Our Quick Links</h6>
            <hr
              class="deep-purple text-primary accent-2 mb-4 mt-0 d-inline-block mx-auto"
            />
            <ul class="list-unstyled mb-0">
              <li>
                <LangLink to="/about"><span>About</span></LangLink>
              </li>
              <li>
                <LangLink to="/contact-us"><span>Contact Us</span></LangLink>
              </li>
              <li>
                <LangLink to="/about"><span>Advanced Search</span></LangLink>
              </li>
            </ul>
          </div>
          <div class="col-lg-3 col-md-12">
            <h6>Contact</h6>
            <hr
              class="deep-purple  text-primary accent-2 mb-4 mt-0 d-inline-block mx-auto"
            />
            <ul class="list-unstyled mb-0">
              <li>
                <a
                  ><i class="fa fa-home mr-3 text-primary"></i> New York, NY
                  10012, US</a
                >
              </li>
              <li>
                <a
                  ><i class="fa fa-envelope mr-3 text-primary"></i>
                  info12323@example.com</a
                >
              </li>
              <li>
                <a
                  ><i class="fa fa-phone mr-3 text-primary"></i> + 01 234 567
                  88</a
                >
              </li>
              <li>
                <a
                  ><i class="fa fa-print mr-3 text-primary"></i> + 01 234 567
                  89</a
                >
              </li>
            </ul>
            <ul class="list-unstyled list-inline mt-3">
              <li class="list-inline-item">
                <a
                  class="btn-floating btn-sm rgba-white-slight mx-1 waves-effect waves-light"
                >
                  <i class="fa fa-facebook bg-facebook"></i>
                </a>
              </li>
              <li class="list-inline-item">
                <a
                  class="btn-floating btn-sm rgba-white-slight mx-1 waves-effect waves-light"
                >
                  <i class="fa fa-twitter bg-info"></i>
                </a>
              </li>
              <li class="list-inline-item">
                <a
                  class="btn-floating btn-sm rgba-white-slight mx-1 waves-effect waves-light"
                >
                  <i class="fa fa-google-plus bg-danger"></i>
                </a>
              </li>
              <li class="list-inline-item">
                <a
                  class="btn-floating btn-sm rgba-white-slight mx-1 waves-effect waves-light"
                >
                  <i class="fa fa-linkedin bg-linkedin"></i>
                </a>
              </li>
            </ul>
          </div>
          <div class="col-lg-4 col-md-12">
            <h6>Subscribe</h6>
            <hr
              class="deep-purple  text-primary accent-2 mb-4 mt-0 d-inline-block mx-auto"
            />
            <div class="clearfix"></div>
            <div class="input-group w-100">
              <input
                type="text"
                class="form-control br-tl-3  br-bl-3 "
                placeholder="Email"
              />
              <div class="input-group-append ">
                <button type="button" class="btn btn-primary br-tr-3  br-br-3">
                  Subscribe
                </button>
              </div>
            </div>
            <h6 class="mb-0 mt-5">Payments</h6>
            <hr
              class="deep-purple  text-primary accent-2 mb-2 mt-3 d-inline-block mx-auto"
            />
            <div class="clearfix"></div>
            <ul class="footer-payments">
              <li class="pl-0">
                <a href="javascript:;"
                  ><i class="fa fa-cc-amex text-muted" aria-hidden="true"></i
                ></a>
              </li>
              <li>
                <a href="javascript:;"
                  ><i class="fa fa-cc-visa text-muted" aria-hidden="true"></i
                ></a>
              </li>
              <li>
                <a href="javascript:;"
                  ><i
                    class="fa fa-credit-card-alt text-muted"
                    aria-hidden="true"
                  ></i
                ></a>
              </li>
              <li>
                <a href="javascript:;"
                  ><i
                    class="fa fa-cc-mastercard text-muted"
                    aria-hidden="true"
                  ></i
                ></a>
              </li>
              <li>
                <a href="javascript:;"
                  ><i class="fa fa-cc-paypal text-muted" aria-hidden="true"></i
                ></a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <div class="bg-dark-purple text-white p-0">
      <div class="container">
        <div class="row d-flex">
          <div class="col-lg-12 col-sm-12 mt-3 mb-3 text-center ">
            Copyright © 2019 <a class="fs-14 text-primary">Solution App</a>.
            Designed by <a class="fs-14 text-primary">Solution App</a>
            All rights reserved.
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>
<script>
export default {
  name: 'Footer'
}
</script>
<style>
.text-center a {
  cursor: pointer;
}
.list-unstyled i {
  transform: translate(0, 0);
}
.list-unstyled span:hover {
  color: #f37e1a;
}
</style>
