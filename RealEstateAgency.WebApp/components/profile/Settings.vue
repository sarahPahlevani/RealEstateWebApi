<template>
  <div class="card mb-0">
    <div class="card-header">
      <h3 class="card-title">Settings</h3>
    </div>
    <div class="card-body">
      <div class="settings-tab">
        <ul class="tabs-menu nav">
          <li class="">
            <a href="#tab1" class="active" data-toggle="tab"
              ><i class="icon icon-speedometer"></i> General</a
            >
          </li>
          <li>
            <a href="#tab2" data-toggle="tab" class=""
              ><i class="icon icon-bubble"></i> Chat</a
            >
          </li>
          <li>
            <a href="#tab3" data-toggle="tab" class=""
              ><i class="icon icon-settings"></i> Advanced</a
            >
          </li>
        </ul>
        <div class="tab-content">
          <div class="tab-pane active show" id="tab1">
            <form class="form-horizontal">
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label" id="examplenameInputname2"
                      >Currency</label
                    >
                  </div>
                  <div class="col-md-9">
                    <select
                      data-placeholder="Choose a Language..."
                      class="form-control select2-show-search custom-select languages"
                    >
                      <option value="0">Choose currency</option>
                      <option value="1">USD</option>
                      <option value="2">INR</option>
                      <option value="3">EUR</option>
                      <option value="4">GBP</option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label" id="examplenameInputname3"
                      >Langague:</label
                    >
                  </div>
                  <div class="col-md-9">
                    <select
                      data-placeholder="Choose a Language..."
                      class="form-control select2-show-search custom-select languages"
                    >
                      <option value="0">Choose Language</option>
                      <option value="AF">Afrikanns</option>
                      <option value="SQ">Albanian</option>
                      <option value="AR">Arabic</option>
                      <option value="HY">Armenian</option>
                      <option value="EU">Basque</option>
                      <option value="BN">Bengali</option>
                      <option value="BG">Bulgarian</option>
                      <option value="CA">Catalan</option>
                      <option value="KM">Cambodian</option>
                      <option value="ZH">Chinese (Mandarin)</option>
                      <option value="HR">Croation</option>
                      <option value="CS">Czech</option>
                      <option value="DA">Danish</option>
                      <option value="NL">Dutch</option>
                      <option value="EN">English</option>
                      <option value="ET">Estonian</option>
                      <option value="FJ">Fiji</option>
                      <option value="FI">Finnish</option>
                      <option value="FR">French</option>
                      <option value="KA">Georgian</option>
                      <option value="DE">German</option>
                      <option value="EL">Greek</option>
                      <option value="GU">Gujarati</option>
                      <option value="HE">Hebrew</option>
                      <option value="HI">Hindi</option>
                      <option value="HU">Hungarian</option>
                      <option value="IS">Icelandic</option>
                      <option value="ID">Indonesian</option>
                      <option value="GA">Irish</option>
                      <option value="IT">Italian</option>
                      <option value="JA">Japanese</option>
                      <option value="JW">Javanese</option>
                      <option value="KO">Korean</option>
                      <option value="LA">Latin</option>
                      <option value="LV">Latvian</option>
                      <option value="LT">Lithuanian</option>
                      <option value="MK">Macedonian</option>
                      <option value="MS">Malay</option>
                      <option value="ML">Malayalam</option>
                      <option value="MT">Maltese</option>
                      <option value="MI">Maori</option>
                      <option value="MR">Marathi</option>
                      <option value="MN">Mongolian</option>
                      <option value="NE">Nepali</option>
                      <option value="NO">Norwegian</option>
                      <option value="FA">Persian</option>
                      <option value="PL">Polish</option>
                      <option value="PT">Portuguese</option>
                      <option value="PA">Punjabi</option>
                      <option value="QU">Quechua</option>
                      <option value="RO">Romanian</option>
                      <option value="RU">Russian</option>
                      <option value="SM">Samoan</option>
                      <option value="SR">Serbian</option>
                      <option value="SK">Slovak</option>
                      <option value="SL">Slovenian</option>
                      <option value="ES">Japanese</option>
                      <option value="SW">Swahili</option>
                      <option value="SV">Swedish </option>
                      <option value="TA">Tamil</option>
                      <option value="TT">Tatar</option>
                      <option value="TE">Telugu</option>
                      <option value="TH">Thai</option>
                      <option value="BO">Tibetan</option>
                      <option value="TO">Tonga</option>
                      <option value="TR">Turkish</option>
                      <option value="UK">Ukranian</option>
                      <option value="UR">Urdu</option>
                      <option value="UZ">Uzbek</option>
                      <option value="VI">Vietnamese</option>
                      <option value="CY">Welsh</option>
                      <option value="XH">Xhosa</option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Phone Number:</label>
                  </div>
                  <div class="col-md-9">
                    <div class="custom-controls-stacked d-md-flex">
                      <label class="custom-control custom-radio mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios3"
                          value="option1"
                          checked
                        />
                        <span class="custom-control-label"
                          >Visible to everyone</span
                        >
                      </label>
                      <label class="custom-control custom-radio">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios3"
                          value="option2"
                        />
                        <span class="custom-control-label"
                          >Visible to only my Customers</span
                        >
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group mb-0">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label" id="inputPassword5"
                      >Profile:</label
                    >
                  </div>
                  <div class="col-md-9">
                    <div class="custom-controls-stacked d-md-flex">
                      <label class="custom-control custom-radio mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios4"
                          value="option1"
                          checked
                        />
                        <span class="custom-control-label"
                          >Visible to everyone</span
                        >
                      </label>
                      <label class="custom-control custom-radio">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios4"
                          value="option2"
                        />
                        <span class="custom-control-label"
                          >Visible to only my Customers</span
                        >
                      </label>
                    </div>
                  </div>
                </div>
              </div>
            </form>
          </div>
          <div class="tab-pane" id="tab2">
            <div class="form-group ">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Font-size:</label>
                </div>
                <div class="col-md-9">
                  <div class="custom-controls-stacked d-sm-flex">
                    <label class="custom-control custom-radio mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios3"
                        value="option1"
                        checked
                      />
                      <span class="custom-control-label">Small</span>
                    </label>
                    <label class="custom-control custom-radio mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios3"
                        value="option2"
                      />
                      <span class="custom-control-label">Medium</span>
                    </label>
                    <label class="custom-control custom-radio">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios3"
                        value="option2"
                      />
                      <span class="custom-control-label">Large</span>
                    </label>
                  </div>
                </div>
              </div>
            </div>
            <div class="form-group ">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Chat:</label>
                </div>
                <div class="col-md-9">
                  <label class="custom-switch pl-0">
                    <input
                      type="checkbox"
                      name="custom-switch-checkbox"
                      class="custom-switch-input"
                    />
                    <span class="custom-switch-indicator"></span>
                    <span class="custom-switch-description">On/Off</span>
                  </label>
                </div>
              </div>
            </div>
            <div class="form-group ">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Chat Type:</label>
                </div>
                <div class="col-md-9">
                  <div class="custom-controls-stacked d-md-flex">
                    <label class="custom-control custom-radio mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios4"
                        value="option1"
                        checked
                      />
                      <span class="custom-control-label">Public Chat</span>
                    </label>
                    <label class="custom-control custom-radio">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios4"
                        value="option2"
                      />
                      <span class="custom-control-label">Private Chat</span>
                    </label>
                  </div>
                </div>
              </div>
            </div>
            <div class="form-group ">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Enable Email Notifications:</label>
                </div>
                <div class="col-md-9">
                  <label class="custom-switch pl-0">
                    <input
                      type="checkbox"
                      name="custom-switch-checkbox"
                      class="custom-switch-input"
                    />
                    <span class="custom-switch-indicator"></span>
                    <span class="custom-switch-description">On/Off</span>
                  </label>
                </div>
              </div>
            </div>
            <div class="form-group ">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Back Up Chat:</label>
                </div>
                <div class="col-md-9">
                  <label class="custom-switchs pl-0">
                    <input
                      type="checkbox"
                      name="custom-switch-checkbox"
                      class="custom-switch-input"
                    />
                    <span class="custom-switch-indicator"></span>
                    <span class="custom-switch-description">On/Off</span>
                  </label>
                </div>
              </div>
            </div>
            <div class="form-group mb-0">
              <div class="row">
                <div class="col-md-3">
                  <label class="form-label">Delete Chat:</label>
                </div>
                <div class="col-md-9">
                  <div class="custom-controls-stacked d-md-flex">
                    <label class="custom-control custom-radio mr-4">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios5"
                        value="option1"
                        checked
                      />
                      <span class="custom-control-label">Delete Read Chat</span>
                    </label>
                    <label class="custom-control custom-radio">
                      <input
                        type="radio"
                        class="custom-control-input"
                        name="example-radios5"
                        value="option2"
                      />
                      <span class="custom-control-label">Delete All</span>
                    </label>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="tab-pane" id="tab3">
            <form class="form-horizontal">
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Auto-Advance:</label>
                  </div>
                  <div class="col-md-9">
                    <label class="custom-switch pl-0">
                      <input
                        type="checkbox"
                        name="custom-switch-checkbox"
                        class="custom-switch-input"
                      />
                      <span class="custom-switch-indicator"></span>
                      <span class="custom-switch-description"
                        >Enable/Disable</span
                      >
                    </label>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label" id="inputEmail4"
                      >Color Theme:</label
                    >
                  </div>
                  <div class="col-md-9">
                    <div class="custom-controls-stacked d-flex">
                      <label class="custom-control custom-radio mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios"
                          value="option1"
                          checked
                        />
                        <span class="custom-control-label">Light</span>
                      </label>
                      <label class="custom-control custom-radio">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios"
                          value="option2"
                        />
                        <span class="custom-control-label">Dark</span>
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Dashboard Theme:</label>
                  </div>
                  <div class="col-md-9">
                    <select
                      data-placeholder="Choose a Language..."
                      class="form-control w-100 custom-select languages"
                    >
                      <option value="0">Choose Theme</option>
                      <option value="1">Default Theme</option>
                      <option value="2">Light Theme</option>
                      <option value="3">Dark Theme</option>
                      <option value="4">Sidebar light Theme</option>
                      <option value="5">Sidebar Dark Theme</option>
                      <option value="6">RTL</option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label" id="inputEmail5"
                      >Posted ADs:</label
                    >
                  </div>
                  <div class="col-md-9">
                    <div class="custom-controls-stacked d-md-flex">
                      <label class="custom-control custom-radio mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios1"
                          value="option1"
                          checked
                        />
                        <span class="custom-control-label">Directly</span>
                      </label>
                      <label class="custom-control custom-radio">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios1"
                          value="option2"
                        />
                        <span class="custom-control-label"
                          >By Admin Conformation</span
                        >
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Comments Visible:</label>
                  </div>
                  <div class="col-md-9">
                    <div class="custom-controls-stacked d-md-flex">
                      <label class="custom-control custom-radio mr-4">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios2"
                          value="option1"
                          checked
                        />
                        <span class="custom-control-label">Directly</span>
                      </label>
                      <label class="custom-control custom-radio">
                        <input
                          type="radio"
                          class="custom-control-input"
                          name="example-radios2"
                          value="option2"
                        />
                        <span class="custom-control-label"
                          >By Admin Conformation</span
                        >
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">System Notification:</label>
                  </div>
                  <div class="col-md-9">
                    <label class="custom-switch d-inline-block mr-5 pl-0">
                      <input
                        type="checkbox"
                        name="custom-switch-checkbox"
                        class="custom-switch-input"
                      />
                      <span class="custom-switch-indicator"></span>
                      <span class="custom-switch-description">On/Off</span>
                    </label>
                    <label class="custom-switch d-inline-block"
                      ><a href="#" class="btn-link"
                        >Add Another Settings</a
                      ></label
                    >
                  </div>
                </div>
              </div>
              <div class="form-group ">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Mail Notification:</label>
                  </div>
                  <div class="col-md-9">
                    <label class="custom-switch d-inline-block mr-5 pl-0">
                      <input
                        type="checkbox"
                        name="custom-switch-checkbox"
                        class="custom-switch-input"
                      />
                      <span class="custom-switch-indicator"></span>
                      <span class="custom-switch-description">On/Off</span>
                    </label>
                    <label class="custom-switch d-inline-block"
                      ><a href="#" class="btn-link"
                        >Add Another Settings</a
                      ></label
                    >
                  </div>
                </div>
              </div>
              <div class="form-group mb-0">
                <div class="row">
                  <div class="col-md-3">
                    <label class="form-label">Auto Watch Comments:</label>
                  </div>
                  <div class="col-md-9">
                    <label class="custom-switch d-inline-block mr-5 pl-0">
                      <input
                        type="checkbox"
                        name="custom-switch-checkbox"
                        class="custom-switch-input"
                      />
                      <span class="custom-switch-indicator"></span>
                      <span class="custom-switch-description">On/Off</span>
                    </label>
                    <label class="custom-switch d-inline-block"
                      ><a href="#" class="btn-link"
                        >Add Another Settings</a
                      ></label
                    >
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <a href="#" class="btn btn-primary">Save Changes</a>
      <a href="#" class="btn btn-secondary">Cancel</a>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Settings'
}
</script>

<style scoped></style>
