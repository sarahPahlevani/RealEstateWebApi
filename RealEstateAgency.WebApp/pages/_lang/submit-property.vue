<template>
  <div>
    <section class="sptb">
      <div class="container">
        <div class="row ">
          <div class="col-lg-8 col-md-12 col-md-12">
            <div class="card ">
              <div class="card-header ">
                <h3 class="card-title">Add Post</h3>
              </div>
              <div class="card-body">
                <div class="form-group">
                  <label class="form-label text-dark">Ad Title</label>
                  <input type="text" class="form-control" placeholder="" />
                </div>
                <div class="form-group">
                  <label class="form-label text-dark">Category</label>
                  <select class="form-control custom-select">
                    <option value="0">Select Option</option>
                    <option value="1">RealEstate</option>
                    <option value="2">Apartments</option>
                    <option value="3">Offices & Fitness</option>
                    <option value="4">2BHK Flats</option>
                    <option value="5">Apartments</option>
                    <option value="6">Laxury Rooms</option>
                    <option value="7">Homes</option>
                    <option value="8">Laxury Homes</option>
                    <option value="9">Modren Houses</option>
                    <option value="10">Home & Furinture</option>
                    <option value="11"> Homes</option>
                    <option value="12">Duplex House</option>
                    <option value="13">Laxury Rooms</option>
                    <option value="14">3BHK Flatss</option>
                  </select>
                </div>
                <div class="form-group">
                  <label class="form-label text-dark">Type Of Ad</label>
                  <div class="d-md-flex ad-post-details">
                    <label class="custom-control custom-radio mb-2 mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios"
                        value="option1"
                        checked=""
                      />
                      <span class="custom-control-label">I Want to Sell</span>
                    </label>
                    <label class="custom-control custom-radio  mb-2 mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios"
                        value="option2"
                      />
                      <span class="custom-control-label">I Want to Buy</span>
                    </label>
                    <label class="custom-control custom-radio  mb-2">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios"
                        value="option3"
                      />
                      <span class="custom-control-label">I Want to Rent</span>
                    </label>
                  </div>
                </div>
                <div class="form-group">
                  <label class="form-label text-dark">Who We Are</label>
                  <div class="d-md-flex ad-post-details">
                    <label class="custom-control custom-radio mb-2 mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios2"
                        value="option4"
                        checked=""
                      />
                      <span class="custom-control-label">Owner </span>
                    </label>
                    <label class="custom-control custom-radio  mb-2 mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios2"
                        value="option5"
                      />
                      <span class="custom-control-label">Agent</span>
                    </label>
                    <label class="custom-control custom-radio  mb-2">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="radios2"
                        value="option6"
                      />
                      <span class="custom-control-label">Builder</span>
                    </label>
                  </div>
                </div>
                <div class="form-group">
                  <label class="form-label text-dark">Description</label>
                  <textarea
                    class="form-control"
                    name="example-textarea-input"
                    rows="6"
                    placeholder="text here.."
                  ></textarea>
                </div>
                <div class="p-2 border mb-4 form-group">
                  <form>
                    <label>Upload Images</label>
                    <input
                      id="demo"
                      type="file"
                      name="files"
                      accept=".jpg, .png, image/jpeg, image/png, html, zip, css,js"
                      multiple
                    />
                  </form>
                </div>
                <div class="row">
                  <div class="col-sm-6 col-md-6">
                    <div class="form-group">
                      <label class="form-label">Name</label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Name"
                      />
                    </div>
                  </div>
                  <div class="col-sm-6 col-md-6">
                    <div class="form-group">
                      <label class="form-label">Email</label>
                      <input
                        type="email"
                        class="form-control"
                        placeholder="Email Address"
                      />
                    </div>
                  </div>
                  <div class="col-sm-6 col-md-6">
                    <div class="form-group mb-0">
                      <label class="form-label">Phone Number</label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Number"
                      />
                    </div>
                  </div>
                  <div class="col-sm-6 col-md-6">
                    <div class="form-group mb-0">
                      <label class="form-label">Address</label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Address"
                      />
                    </div>
                  </div>
                </div>
                <div class="border p-3 mt-4">
                  <div class="control-group form-group mb-0">
                    <label class="form-label text-dark">Ad Post Package</label>
                    <div class="d-md-flex ad-post-details">
                      <label class="custom-control custom-radio mb-2 mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="radios1"
                          value="option7"
                          checked=""
                        />
                        <span class="custom-control-label">30 Days Free</span>
                      </label>
                      <label class="custom-control custom-radio  mb-2 mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="radios1"
                          value="option8"
                        />
                        <span class="custom-control-label"
                          >60 days /
                          <span class="font-weight-bold">$20</span></span
                        >
                      </label>
                      <label class="custom-control custom-radio  mb-2 mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="radios1"
                          value="option9"
                        />
                        <span class="custom-control-label"
                          >6months /<span class="font-weight-bold"
                            >$50</span
                          ></span
                        >
                      </label>
                      <label class="custom-control custom-radio  mb-2">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="radios1"
                          value="option10"
                        />
                        <span class="custom-control-label"
                          >1 year /
                          <span class="font-weight-bold">$80</span></span
                        >
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer ">
                <a href="#" class="btn btn-success">Submit Now</a>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-12">
            <div class="card">
              <div class="card-header">
                <h3 class="card-title">Benefits Of Premium Ad</h3>
              </div>
              <div class="card-body pb-2">
                <ul class="list-unstyled widget-spec vertical-scroll mb-0">
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium Ads Active
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are displayed on top
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads will be Show in Google results
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium Ads Active
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are displayed on top
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads will be Show in Google results
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium Ads Active
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are displayed on top
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads will be Show in Google results
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium Ads Active
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are displayed on top
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads will be Show in Google results
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium Ads Active
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are displayed on top
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads will be Show in Google results
                  </li>
                </ul>
              </div>
            </div>
            <div class="card">
              <div class="card-header">
                <h3 class="card-title">Terms And Conditions</h3>
              </div>
              <div class="card-body">
                <ul class="list-unstyled widget-spec  mb-0">
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Money Not Refundable
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >You can renew your Premium ad after experted.
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i
                    >Premium ads are active for depend on package.
                  </li>
                  <li class="ml-5 mb-0">
                    <a href="tips.html"> View more..</a>
                  </li>
                </ul>
              </div>
            </div>
            <div class="card mb-0">
              <div class="card-header">
                <h3 class="card-title">Safety Tips For Buyers</h3>
              </div>
              <div class="card-body">
                <ul class="list-unstyled widget-spec  mb-0">
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i>
                    Meet Seller at public Place
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i>
                    Check item before you buy
                  </li>
                  <li>
                    <i class="fa fa-check text-success" aria-hidden="true"></i>
                    Pay only after collecting item
                  </li>
                  <li class="ml-5 mb-0">
                    <a href="tips.html"> View more..</a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
          <div class="col-xl-8">
            <div class="card mb-xl-0">
              <div class="card-header">
                <h3 class="card-title">Payments</h3>
              </div>
              <div class="card-body">
                <div class="tab-content card-body border mb-0 b-0">
                  <div class="panel panel-primary">
                    <div class=" tab-menu-heading border-0 pl-0 pr-0 pt-0">
                      <div class="tabs-menu1 ">
                        <!-- Tabs -->
                        <ul class="nav panel-tabs">
                          <li>
                            <a href="#tab5" class="active" data-toggle="tab"
                              >Credit/ Debit Card</a
                            >
                          </li>
                          <li><a href="#tab6" data-toggle="tab">Paypal</a></li>
                          <li>
                            <a href="#tab7" data-toggle="tab">Net Banking</a>
                          </li>
                          <li>
                            <a href="#tab8" data-toggle="tab">Gift Voucher</a>
                          </li>
                        </ul>
                      </div>
                    </div>
                    <div class="panel-body tabs-menu-body pl-0 pr-0 border-0">
                      <div class="tab-content">
                        <div id="tab5" class="tab-pane active ">
                          <div class="form-group">
                            <label class="form-label">CardHolder Name</label>
                            <input
                              id="name1"
                              type="text"
                              class="form-control"
                              placeholder="First Name"
                            />
                          </div>
                          <div class="form-group">
                            <label class="form-label">Card number</label>
                            <div class="input-group">
                              <input
                                type="text"
                                class="form-control"
                                placeholder="Search for..."
                              />
                              <span class="input-group-append">
                                <button class="btn btn-info" type="button">
                                  <i class="fa fa-cc-visa"></i> &nbsp;
                                  <i class="fa fa-cc-amex"></i> &nbsp;
                                  <i class="fa fa-cc-mastercard"></i>
                                </button>
                              </span>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-sm-8">
                              <div class="form-group mb-sm-0">
                                <label class="form-label">Expiration</label>
                                <div class="input-group">
                                  <input
                                    type="number"
                                    class="form-control"
                                    placeholder="MM"
                                    name="expiremonth"
                                  />
                                  <input
                                    type="number"
                                    class="form-control"
                                    placeholder="YY"
                                    name="expireyear"
                                  />
                                </div>
                              </div>
                            </div>
                            <div class="col-sm-4 ">
                              <div class="form-group mb-0">
                                <label class="form-label"
                                  >CVV
                                  <i
                                    class="fa fa-question-circle"
                                    data-toggle="tooltip"
                                    data-placement="top"
                                    title="Please Enter last 3 digits"
                                  ></i
                                ></label>
                                <input
                                  type="number"
                                  class="form-control"
                                  required=""
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                        <div id="tab6" class="tab-pane">
                          <h6 class="font-weight-semibold">
                            Paypal is easiest way to pay online
                          </h6>
                          <p>
                            <a href="#" class="btn btn-primary"
                              ><i class="fa fa-paypal"></i> Log in my Paypal</a
                            >
                          </p>
                          <p class="mb-0">
                            <strong>Note:</strong> Nemo enim ipsam voluptatem
                            quia voluptas sit aspernatur aut odit aut fugit, sed
                            quia consequuntur magni dolores eos qui ratione
                            voluptatem sequi nesciunt.
                          </p>
                        </div>
                        <div class="tab-pane" id="tab7">
                          <div class="control-group form-group">
                            <div class="form-group">
                              <label class="form-label text-dark"
                                >All Banks</label
                              >
                              <select
                                class="form-control custom-select required category"
                              >
                                <option value="0">Select Bank</option>
                                <option value="1">Credit Agricole Group</option>
                                <option value="2">Bank of America</option>
                                <option value="3"
                                  >Mitsubishi UFJ Financial Group</option
                                >
                                <option value="4">BNP Paribas</option>
                                <option value="5">JPMorgan Chase & Co.</option>
                                <option value="6">HSBC Holdings</option>
                                <option value="7">Bank of China</option>
                                <option value="8"
                                  >Agricultural Bank of China</option
                                >
                                <option value="9">ChinaFlats Bank Corp.</option>
                                <option value="10"
                                  >Industrial & Commercial Bank of China, or
                                  ICBC</option
                                >
                              </select>
                            </div>
                          </div>
                          <p>
                            <a href="#" class="btn btn-primary">Log in Bank</a>
                          </p>
                        </div>
                        <div id="tab8" class="tab-pane">
                          <div class="form-group">
                            <label class="form-label">Gift Voucher</label>
                            <div class="input-group">
                              <input
                                type="text"
                                class="form-control"
                                placeholder="Enter Your Gv Number"
                              />
                              <span class="input-group-append">
                                <button class="btn btn-info" type="button">
                                  Apply
                                </button>
                              </span>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="form-group row clearfix">
                    <div class="col-lg-12">
                      <div class="checkbox checkbox-info">
                        <label class="custom-control mt-4 custom-checkbox">
                          <input type="checkbox" class="custom-control-input" />
                          <span class="custom-control-label text-dark pl-2"
                            >I agree with the Terms and Conditions.</span
                          >
                        </label>
                      </div>
                    </div>
                    <ul class=" mb-b-4 ">
                      <li class="float-right">
                        <a href="#" class="btn btn-primary  mb-0 mr-2"
                          >Continue</a
                        >
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!--/Section-->

    <!-- Newsletter-->
    <section class="sptb bg-white border-top">
      <div class="container">
        <div class="row">
          <div class="col-lg-7 col-xl-6 col-md-12">
            <div class="sub-newsletter">
              <h3 class="mb-2">
                <i class="fa fa-paper-plane-o mr-2"></i> Subscribe To Our
                Newsletter
              </h3>
              <p class="mb-0">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor
              </p>
            </div>
          </div>
          <div class="col-lg-5 col-xl-6 col-md-12">
            <div class="input-group sub-input mt-1">
              <input
                type="text"
                class="form-control input-lg "
                placeholder="Enter your Email"
              />
              <div class="input-group-append ">
                <button
                  type="button"
                  class="btn btn-primary btn-lg br-tr-3  br-br-3"
                >
                  Subscribe
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  middleware: 'authenticated'
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
