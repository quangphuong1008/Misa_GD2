<template>
  <div class="m-popup" v-show="isshow">
    <div
      class="m-popup-content"
      :style="[{ width: '900px' }, { 'border-radius': 0 }]"
    >
      <div class="m-popup__header">
        <div class="m-popup-tittle">Thông tin nhà cung cấp</div>
        <div class="al_center">
          <BaseRadioGroup
            v-model="accountMode"
            class="custom-padding"
            :items="listAccount"
          />
        </div>
        <div class="al_center">
          <Checkbox class="custom-padding" :label="'Là khách hàng'" />
        </div>
        <div class="m-popup-close">
          <div class="m-popup-btn help" @click="handleHelpPopup"></div>
          <div class="m-popup-btn close" @click="handleClosePopup"></div>
        </div>
      </div>
      <div class="m-popup-body">
        <div class="grid__row" style="position: relative">
          <div class="w-1/2 p-r-26">
            <div class="grid__row">
              <TextField
                :disable="disableMode"
                class="w-2/5 p-r-6"
                :label="'Mã số thuế'"
                v-model="taxCode"
                ref="taxCode"
              />
              <TextField
                :disable="disableMode"
                class="w-3/5"
                :label="'Mã nhà cung cấp'"
                :required="true"
                v-model="accountObjectCode"
                ref="accountObjectCode"
                :checkInput="checkInput('accountObjectCode', accountObjectCode)"
                :errorText="resource.VN.Warning.AccountObjectCodeIsEmpty"
              />
            </div>
            <TextField
              :disable="disableMode"
              class="grid__row"
              :label="'Tên nhà cung cấp'"
              ref="accountObjectName"
              v-model="accountObjectName"
              :required="true"
              :checkInput="checkInput('accountObjectName', accountObjectName)"
              :errorText="resource.VN.Warning.AccountObjectNameIsEmpty"
            />
            <TextField
              :disable="disableMode"
              class="grid__row"
              :label="'Địa chỉ'"
              :inputType="'textarea'"
              v-model="address"
              :height="'55px'"
              :placeholder="resource.VN.Example.Address"
              ref="address"
            />
          </div>
          <div
            class="w-1/2"
            style="height: 194.3px; position: absolute; top: 0px; left: 50%"
          >
            <div class="grid__row">
              <TextField
                :disable="disableMode"
                class="w-2/5 p-r-6"
                :label="'Điện thoại'"
                v-model="phone"
                ref="phone"
              />
              <TextField
                :disable="disableMode"
                class="w-3/5"
                :label="'Webside'"
                v-model="webSite"
              />
            </div>
            <ComboboxAccObjGroup
              :disable="disableMode"
              class="grid__row"
              :label="'Nhóm nhà cung cấp'"
              :fields="fieldAccObjGroup"
              :datas="dataAccountObjecGroups"
              :id="'accountObjectGroupCode'"
              :textShow="'accountObjectGroupCode'"
              :icon="true"
              v-model="accountObjectGroupListId"
              ref="accountObjectGroupListId"
            />

            <BaseCombobox
              :disable="disableMode"
              :label="'Nhân viên'"
              :fields="fieldEmployee"
              :datas="dataEmployees"
              v-model="employeeId"
              :id="'employeeId'"
              :textShow="'fullName'"
              :icon="true"
              ref="employeeId"
            />
          </div>
        </div>
        <div class="popup-tab">
          <div class="tab">
            <ul class="ul-tab">
              <li
                class="ms-tabs-li"
                :class="slotTab == 1 || slotTab == 0 ? 'select' : ''"
              >
                <div type="button" class="ms-tabs-btn" @click="slotTab = 1">
                  Liên hệ
                </div>
              </li>
              <li class="ms-tabs-li" :class="slotTab == 2 ? 'select' : ''">
                <div type="button" class="ms-tabs-btn" @click="slotTab = 2">
                  Điều khoản thanh toán
                </div>
              </li>
              <li class="ms-tabs-li" :class="slotTab == 3 ? 'select' : ''">
                <div type="button" class="ms-tabs-btn" @click="slotTab = 3">
                  Tài khoản ngân hàng
                </div>
              </li>
              <li class="ms-tabs-li" :class="slotTab == 4 ? 'select' : ''">
                <div type="button" class="ms-tabs-btn" @click="slotTab = 4, editLocation=true">
                  Địa chỉ khác
                </div>
              </li>
              <li class="ms-tabs-li" :class="slotTab == 5 ? 'select' : ''">
                <div type="button" class="ms-tabs-btn" @click="slotTab = 5">
                  Ghi chú
                </div>
              </li>
            </ul>
          </div>
          <div class="slot-tab">
            <div v-show="slotTab == 1 || slotTab == 0">
              <div class="grid__row" style="position: relative; margin: 10px">
                <div class="w-1/2 p-r-26">
                  <div class="grid__row" style="position: relative">
                    <BaseCombobox
                      :disable="disableMode"
                      class="w-2/5 p-r-6"
                      :label="'Người liên hệ'"
                      :noneThead="true"
                      :fields="[{ name: 'value' }]"
                      :datas="listPrefix"
                      v-model="prefix"
                      :textShow="'value'"
                      :id="'value'"
                      ref="prefix"
                    />

                    <TextField
                      :disable="disableMode"
                      class="w-3/5"
                      style="position: absolute; top: 21px; right: 0"
                      :placeholder="'Họ và tên'"
                      v-model="einvoiceContactName"
                      ref="einvoiceContactName"
                    />
                  </div>
                  <TextField
                    :disable="disableMode"
                    class="grid__row"
                    :placeholder="'Email'"
                    v-model="einvoiceContactEmail"
                    ref="einvoiceContactEmail"
                    :checkInput="
                      checkInput('einvoiceContactEmail', einvoiceContactEmail)
                    "
                    :errorText="'Email sai định dạng'"
                  />
                  <TextField
                    :disable="disableMode"
                    class="grid__row w-1/2"
                    :placeholder="'Số điện thoại'"
                    v-model="einvoiceContactMobile"
                    ref="einvoiceContactMobile"
                  />
                </div>
                <div
                  class="w-1/2"
                  style="
                    height: 194.3px;
                    position: absolute;
                    top: 0px;
                    left: 50%;
                  "
                >
                  <div class="grid__row">
                    <TextField
                      :disable="disableMode"
                      class="grid__row"
                      :label="'Đại diện theo PL'"
                      v-model="legalRepresentative"
                      ref="legalRepresentative"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div v-show="slotTab == 2">
              <div
                class="grid__row"
                style="
                  position: relative;
                  margin: 10px;
                  justify-content: normal;
                "
              >
                <BaseCombobox
                  :disable="disableMode"
                  class="grid__row w-1/4 p-r-6"
                  style="margin-bottom: -13px"
                  :label="'Điều khoản thanh toán'"
                  :fields="fieldPaymentTerm"
                  :datas="dataPaymentTerms"
                  :id="'paymentTermId'"
                  :textShow="'paymentTermCode'"
                  v-model="paymentTermId"
                  :icon="true"
                  :width="'500px'"
                  ref="paymentTermId"
                />

                <TextField
                  :disable="disableMode"
                  class="w-1/4 p-r-6"
                  :label="'Số ngày được nợ'"
                  v-model="dueTime"
                  ref="dueTime"
                />

                <TextField
                  :disable="disableMode"
                  class="w-1/4 p-r-6"
                  :label="'Số nợ tối đa'"
                  v-model="maximizeDebtAmount"
                  ref="maximizeDebtAmount"
                />
              </div>
              <div
                class="grid__row"
                style="
                  position: relative;
                  margin: 10px;
                  justify-content: normal;
                "
              >
                <BaseCombobox
                  :disable="disableMode"
                  class="grid__row w-1/4 p-r-6"
                  style="margin-bottom: -13px"
                  :label="'Tài khoản nợ công phải trả'"
                  :fields="fieldPayAccounts"
                  :datas="payAccounts"
                  :id="'pay_account_code'"
                  :textShow="'pay_account_code'"
                  v-model="payAccount"
                  :icon="false"
                  :width="'350px'"
                  ref="payAccount"
                />
              </div>
            </div>
            <div v-show="slotTab == 3">
              <BaseTableFocus
               :disable="disableMode"
                style="width: 100%;padding: 0 8px"
                :noneFooter="true"
                :noneStt="true"
                :fields="fieldTableBank"
                v-model="accountObjectBankAccount"
                ref="accountObjectBankAccount"
              />
            </div>
            <div v-show="slotTab == 4">
              <div class="grid__row" style="position: relative; margin: 10px">
                <div class="w-1/2 p-r-26">
                  <div class="grid__row" style="position: relative">
                    <BaseCombobox
                      :disable="disableMode"
                      class="w-1/2 p-r-6"
                      :label="'Vị trí địa lý'"
                      :noneThead="true"
                      v-model="country"
                      :fields="[{ name: 'location_name' }]"
                      :datas="countryList"
                      :id="'location_id'"
                      :textShow="'location_name'"
                      :placeholder="'Quốc gia'"
                      ref="country"
                    />
                    <BaseCombobox
                      :disable="disableMode"
                      style="position: absolute; top: 21px; right: 0"
                      class="w-1/2"
                      :placeholder="'Tỉnh/Thành Phố'"
                      :noneThead="true"
                      v-model="provinceOrCity"
                      :datas="provinceOrCityList"
                      :fields="[{ name: 'location_name' }]"
                      :id="'location_id'"
                      :textShow="'location_name'"
                    />
                  </div>
                  <div class="grid__row">
                    <BaseCombobox
                      :disable="disableMode"
                      class="w-1/2 p-r-6"
                      :noneThead="true"
                      :placeholder="'Quận/Huyện'"
                      v-model="district"
                      :datas="districtList"
                      :fields="[{ name: 'location_name' }]"
                      :id="'location_id'"
                      :textShow="'location_name'"
                    />
                    <BaseCombobox
                      :disable="disableMode"
                      class="w-1/2"
                      :noneThead="true"
                      :placeholder="'Xã/Phường'"
                      v-model="wardOrCommune"
                      :fields="[{ name: 'location_name' }]"
                      :datas="wardOrCommuneList"
                      :id="'location_id'"
                      :textShow="'location_name'"
                    />
                  </div>
                </div>
                <div
                  class="w-1/2"
                  style="
                    height: 194.3px;
                    position: absolute;
                    top: 0px;
                    left: 50%;
                  "
                >
                  <div
                    class="grid__row "
                    style="padding-bottom: 0; height: 15px;     justify-content: flex-start;"
                  >
                    <div class="m-text-tittle">Địa chỉ giao hàng</div>
                    <div @change="coppyAddres = !coppyAddres">
                      <Checkbox
                        :label="'Giống địa chỉ nhà cung cấp'"
                        style="margin-left: 18px"
                      />
                    </div>
                  </div>
                  <div class="grid__row">
                    <BaseTableFocus
                      :noneStt="true"
                      :noneFooter="true"
                      :noneThead="true"
                      :fields="fieldTableAddress"
                      v-model="accountObjectShippingAddress"
                      ref="accountObjectShippingAddress"
                      :disable="disableMode"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div v-show="slotTab == 5">
              <textarea
                class="m-text-tab"
                v-model="description"
                ref="description"
                :disable="disableMode"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="m-popup-footer">
        <div class="divide"></div>
        <div class="grid__row" style="margin-bottom: 10px; direction: rtl">
          <div>
            <Button
              class="m-popup-footer-btn"
              :backgroundcolor="'#2ca01c'"
              :color="'#fff'"
              @handleClick="handleBtnSaveAndAdd"
              >Cất và Thêm</Button
            >
            <Button
              class="m-popup-footer-btn p-r-12"
              @handleClick="handleBtnSave"
              style="color: black; background: #ffffff; margin-right: 12px"
              >Cất</Button
            >
          </div>
          <div>
            <Button
              class="m-popup-footer-btn"
              :color="'transparent'"
              style="color: black; background: #ffffff"
              @click="distroyPopup"
              >Hủy</Button
            >
          </div>
        </div>
      </div>
    </div>

    <MessageBox
      v-if="showMsg"
      @handleConfirmMessage="handleConfirmMessage"
      @handleCanclemMessage="
        deletes = false;
        showMsg = false;
        doFocus();
      "
      :type="MessageBoxType"
      :width="'444px'"
      :text="MesageBoxText"
    />
    <MessageBoxCloseForm
      v-if="showMsgCloseForm"
      :width="'444px'"
      :text="MesageBoxText"
      @handleCanclemMessage="showMsgCloseForm = false"
      @handleNoConfirmMessage="handleNoConfirmMessage"
      @handleConfirmMessage="handleBtnSave"
    />

    <div id="toast"></div>
  </div>
</template>
<script>
import axios from "axios";
import Checkbox from "../../components/base/BaseCheckbox.vue";
import Button from "../../components/base/BaseButton.vue";
import TextField from "../../components/base/BaseTextField.vue";
import BaseRadioGroup from "../../components/base/BaseRadioGroup.vue";
import BaseCombobox from "../../components/account_object/ComboboxAccObj.vue";
import BaseTableFocus from "../../components/ca_payment/BaseTableFocus.vue";
import ComboboxAccObjGroup from "../../components/account_object/ComboboxAccObjGroup.vue";
import Location from "../../assets/js/location";
import Toast from "../../assets/js/toast";
import Resource from "../../assets/js/account_object/resource";
import Enum from "../../assets/js/enum";
import MessageBox from "../../components/base/BaseMessageBox.vue";
import MessageBoxCloseForm from "../../components/custom/MessageBoxCloseForm.vue";

export default {
  components: {
    Checkbox,
    Button,
    TextField,
    BaseRadioGroup,
    BaseCombobox,
    BaseTableFocus,
    // TableAddress,
    ComboboxAccObjGroup,
    MessageBox,
    MessageBoxCloseForm,
  },
  props: {
    formMode: Number,
    focusId: String,
    dataAccountObjecGroups: Array,
    dataPaymentTerms: Array,
    dataEmployees: Array,
  },

  data() {
    return {
      slotTab: 0,
      accountMode: "0",
      fieldTableAddress: [
        {
          name: "address",
          text: "Số tài khoản",
        },
      ],

      fieldTableBank: [
        {
          name: "bank_account",
          text: "Số tài khoản",
        },
        {
          name: "bank_branch_name",
          text: "Tên ngân hàng",
        },
        {
          name: "bank_name",
          text: "Chi nhánh",
        },
        {
          name: "bank_province_or_city",
          text: "tỉnh/tp của ngân hàng",
        },
      ],
      listAccount: [
        {
          id: "0",
          name: "Tổ chức",
        },
        {
          id: "1",
          name: "Cá nhân",
        },
      ],

      listPrefix: [
        {
          value: "Anh",
        },
        {
          value: "Chị",
        },
      ],
      temp: null,
      // combobox nhân viên mua hàng
      fieldEmployee: [
        {
          name: "employeeCode",
          text: "Mã nhân viên",
        },
        {
          name: "fullName",
          text: "Tên nhân viên",
        },
      ],
      employee: [
        {
          employeeCode: "1",
          fullName: "hung",
        },
        {
          employeeCode: "2",
          fullName: "Quang",
        },
      ],
      // combobox nhóm nhà cc
      fieldAccObjGroup: [
        {
          name: "accountObjectGroupCode",
          text: "Mã nhóm KH,NCC",
        },
        {
          name: "accountObjectGroupName",
          text: "Tên nhóm KH,NCC",
        },
      ],

      // combobox điều khoản thanh toán
      fieldPaymentTerm: [
        {
          name: "paymentTermCode",
          text: "Mã điều khoản thanh toán",
        },
        {
          name: "paymentTermName",
          text: "Tên điều khoản thanh toán",
        },
      ],

      // combobox tài khoản nợ công
      fieldPayAccounts: [
        {
          name: "pay_account_code",
          text: "Số tài khoản",
        },
        {
          name: "pay_account_name",
          text: "Tên tài khoản",
        },
      ],
      payAccounts: [
        {
          pay_account_code: "331",
          pay_account_name: "Phải trả cho người bán",
        },
        {
          pay_account_code: "3348",
          pay_account_name: "Phải trả cho người lao động khác",
        },
        {
          pay_account_code: "555",
          pay_account_name: "Phải trả cho nhân viên",
        },
      ],

      resource: Resource,

      accountObjectId: "", //id nhà cung cấp
      taxCode: "", // mã số thuế
      accountObjectCode: "", // mã nhà cung cấp
      accountObjectName: "", // tên nhà cung cấp
      phone: "", // số điện thoại
      webSite: "", // webSide
      accountObjectGroupListId: [], // danh sách nhóm nhà cung cấp
      address: "", // địa chỉ
      employeeId: "", // id nhân viên
      prefix: null, // xưng hô người liên hện
      einvoiceContactName: "", // họ và tên người liên hệ
      einvoiceContactMobile: "", // số điện thoại người liên hệ
      einvoiceContactEmail: "", // Email người liên hệ
      legalRepresentative: "", // dại diện theo PL
      paymentTermId: "", // id điều khoản Thanh toán
      maximizeDebtAmount: "", // nợ tối đa
      dueTime: "", // số ngày được nợ
      payAccount: "", // mã tài khoản nợ công phải trả
      accountObjectBankAccount: [{}],
      accountObjectShippingAddress: [{}],
      //  accountObjectBankAccount: `[{"bank_account":"qansadasdg","bank_branch_name":"adsa","bank_name":"s","bank_province_or_city":"d"},{"bank_account":"s","bank_branch_name":"d","bank_name":"s","bank_province_or_city":"s"}]`, // thông tin ngân hàng
      //   accountObjectShippingAddress: `[{"address":"ádad"},{"address":"ádasdaad"}]`, // danh sách địa chỉ
      country: "", // đất nước
      provinceOrCity: "", // tỉnh thành phố
      district: "", // quận huyện
      wardOrCommune: "", //phường xã
      description: "", // ghi chú

      //// Vị trí
      editLocation: false,
      loaction: Location,
      countryList: [],
      provinceOrCityList: [],
      districtList: [],
      wardOrCommuneList: [],
      ////
      coppyAddres: false,
      /// validate

      ValidateMode: false,
      FocusInput: false,
      ///
      disableMode: false,
      // dữ liệu để check khi thay đổi
      accountObjectString: "",
      //
      tempAccountObject: {},
      listAccountGroupIdString: "",

      //show form
      showMsg: false, // show Message
      showMsgCloseForm: false, //

      isshow: false,

      MesageBoxText: "",
      saveAdd :  false,
    };
  },
  mounted() {
    this.country = "1";

    this.countryList = Location["VN"].countries;
    this.$refs.taxCode.$el.querySelector("input").focus();

  },
  methods: {
       /**
     * Hàm xử lý sự kiện update danh sách nhóm nhà cung cấp
     * @Author nmquang 27-01-2022
     */
    updateListAccountGroupIdString() {
      this.listAccountGroupIdString = "";
      this.accountObjectGroupListId.forEach((item) => {
        this.listAccountGroupIdString += item + ",";
      });
      this.listAccountGroupIdString = this.listAccountGroupIdString.slice(
        0,
        this.listAccountGroupIdString.length - 1
      );
    },
   /**
     * Hàm xử lý sự kiện khi bấm hủy message
     * @Author nmquang 27-01-2022
     */
    handleNoConfirmMessage() {
      let me = this;
      me.showMsgCloseForm = false;
      me.distroyPopup();
      me.RefreshAccountObject();
    },
     /**
     * Hàm xử lý sự kiện khi bấm các phím tắt
     * @Author nmquang 27-01-2022
     */
    handleKeyUpList(e) {
      // bấm
      if (e.ctrlKey && e.keyCode == 83) {
        this.handleBtnSave();
        this.$parent.$refs.btnAdd.$el.querySelector("button").blur();
        e.preventDefault(); // hủy event mặc định
        e.stopPropagation(); // tránh liên quan đến event khác
      } else if (e.keyCode == 27) {
        e.preventDefault(); // hủy event mặc định
        e.stopPropagation(); // tránh liên quan đến event khác
        this.handleClosePopup();
        this.$parent.$refs.btnAdd.$el.querySelector("button").blur();
      }
      // //Shift N để mở form thêm mới.
      // else if(e.shiftKey &&  e.keyCode == 78)
      // {
      //   e.preventDefault(); // hủy event mặc định
      //   e.stopPropagation(); // tránh liên quan đến event khác
      // }
      // //Shift E để mở form sửa
      // else if(e.shiftKey &&  e.keyCode == 69)
      // {
      //   e.preventDefault();
      //   e.stopPropagation();
      // }
      // //Shift D để mở form xóa
      // else if(e.shiftKey &&  e.keyCode == 68)
      // {
      //   e.preventDefault();
      //   e.stopPropagation();
      // }
      // //Shift R để resetfata
      // else if(e.shiftKey &&  e.keyCode == 82)
      // {
      //   e.preventDefault();
      //   e.stopPropagation();
      // }
    },
      /**
     * Hàm làm mới dữ liệu đổ lên table
     * @Author nmquang 19-12-2021
     */
    reLoadData() {
      this.$emit("reLoadData");
    },
   /**
     * Hàm xử lý sự kiện khi bấm nút help
     * @Author nmquang 27-01-2022
     */
    handleHelpPopup() {},
     /**
     * Hàm xử lý sự kiện khi bấm close Popup
     * @Author nmquang 27-01-2022
     */
    handleClosePopup() {
      let me = this;
      me.GetDataAccountObject();
      let checkAccountToString = JSON.stringify(me.tempAccountObject);
      if (me.formMode != Enum.FormMode.Show && checkAccountToString != me.accountObjectString) {
        me.showMsgCloseForm = true;
      } else {
        me.distroyPopup();
        me.reLoadData();
      }

      this.disableMode = false;
    },
  /**
     * Hàm xử lý sự kiện khi close Popup
     * @Author nmquang 27-01-2022
     */
    distroyPopup() {
      this.editLocation = false;
      this.$emit("handleClosePopup");
    },
    /**
     * Hàm xử lý cất và thêm
     * @Author nmquang(22/12/2021)
     */
    async handleBtnSaveAndAdd() {

      var me = this;
      me.saveAdd = true;
      try {
        let saveSuccess = await me.handleBtnSave();
        if (saveSuccess) {
          await this.$emit("saveAndAdd");
        } else {
          me.ValidateMode = true;
        }
      } catch (error) {
        new Toast(
          this.$parent.$refs.toast,
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm xử lý sự kiện nhấn nút thêm nhân viên mới
     * @Author nmquang 19-12-2021
     */
    async handleBtnAdd() {
      var me = this;
      me.ValidateMode = false;
      me.formMode = Enum.FormMode.Add;
      me.RefreshAccountObject();
      await this.getNewCode();
      me.showPopup = true;
      me.FocusInput = false;
      me.GetDataAccountObject();
      me.accountObjectString = JSON.stringify(me.tempAccountObject);
    },
    async handleBtnSave() {
      var me = this;
      me.showMsgCloseForm = false;
      // Validate dữ liệu:
      let validate = me.checkValidate();
      //  let validate = true;
      me.FocusInput = false;
      let isAvailable;
      //-----------------
      if (validate) {
        me.GetDataAccountObject();
        if (
          me.formMode == Enum.FormMode.Add ||
          me.formMode == Enum.FormMode.Clone
        ) {
          isAvailable = await me.checkAvailableEmployeeCode();
          if (isAvailable) {
            me.showMsg = true;
            me.MesageBoxText = Resource["VN"].Warning.AccountObjectCodeIsAvailable;
            me.MessageBoxType = "error";
          } else {
            // Thực hiện thêm nhân viên
            try {
              await me.callApiCreate(me.tempAccountObject);
              new Toast(
                this.$parent.$refs.toast,
                Resource["VN"].ToastMessage.AddSuccess
              );
              if (!me.saveAdd) {
                me.distroyPopup();
                me.reLoadData();
              }
            } catch (error) {
              new Toast(
                this.$parent.$refs.toast,
                Resource["VN"].ToastMessage.Error,
                "error"
              );
            }
          }
        } else if (me.formMode == Enum.FormMode.Edit) {
          isAvailable = await me.checkAvailableEmployeeCode();
          if (isAvailable) {
            me.showMsg = true;
            me.MesageBoxText = Resource["VN"].Warning.AccountObjectCodeIsAvailable;
            me.MessageBoxType = "error";
          } else {
            try {
              me.tempAccountObject.accountObjectId = me.focusId;
              await me.callApiUpdate(me.focusId, me.tempAccountObject);

              new Toast(
                this.$parent.$refs.toast,
                Resource["VN"].ToastMessage.UpdateSuccess
              );
             if (!me.saveAdd) {
                me.distroyPopup();
                me.reLoadData();
              }

              // me.CallApiEmployeePaging();
            } catch (error) {
              new Toast(
                this.$parent.$refs.toast,
                Resource["VN"].ToastMessage.Error,
                "error"
              );
            }
          }
        }
        return true;
      }
      return false;
    },
      /**
     * Hàm gọi APi thêm mới
     * @Author nmquang 19-12-2021
     */
    async callApiCreate(accountObject) {
      try {
        const res = await axios.post(
          `${Resource.AMIS_SERVICE_URL}/AccountObject`,
          accountObject
        );
        return res;
      } catch (error) {
        new Toast(
          this.$parent.$refs.toast,
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm gọi Api update
     * @Author nmquang 19-12-2021
     */
    async callApiUpdate(id, AccountObject) {
      try {
        const res = await axios.put(
          `${Resource.AMIS_SERVICE_URL}/AccountObject/${id}`,
          AccountObject
        );
        return res;
      } catch (error) {
        new Toast(
          this.$parent.$refs.toast,
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },

    /**
     * check trùng id
     */
    async checkAvailableEmployeeCode() {
      var me = this;
      let isAvailable = false;
      await axios
        .get(
          `${Resource.AMIS_SERVICE_URL}/AccountObject/GetByCode?EntityCode=${me.accountObjectCode}`
        )
        .then((res) => {
          if (res.data.data) {
            if (me.formMode == Enum.FormMode.Edit) {
              if (res.data.data.accountObjectId == me.focusId) {
                isAvailable = false;
              } else {
                isAvailable = true;
              }
            } else {
              isAvailable = true;
            }
          }
        });
      return isAvailable;
    },

    handleConfirmMessage() {},

    /**
     * Hàm check điều kiện và hiển thị Input báo đỏ
     * @Author nmquang 19-12-2021
     */
    checkInput(nameRef, value) {
      var me = this;
      let valid = false;
      if (nameRef == "einvoiceContactEmail") {
        if (
          me.ValidateMode == true &&
          value != "" &&
          String(value).indexOf("@") == -1
        ) {
          valid = true;
        } else {
          valid = false;
        }
      } else {
        if (me.ValidateMode == true && value == "") {
          valid = true;
        } else {
          valid = false;
        }
      }

      return valid;
    },
    /**
     * Hàm Focus vào ô nhập liệu
     * @Author nmquang 19-12-2021
     */
    doFocus() {
      var me = this;
      if (me.formMode != null) {
        let ListFocus = [
          "accountObjectCode",
          "accountObjectName",
          "einvoiceContactEmail",
        ];
        ListFocus.forEach((nameRef) => {
          if (me.FocusInput == false && nameRef == "einvoiceContactEmail") {
            {
              if (
                me.ValidateMode == true &&
                this[nameRef] != "" &&
                String(this[nameRef]).indexOf("@") == -1
              ) {
                me.$refs[nameRef].$el.querySelector("input").focus();
                me.FocusInput = true;
              }
            }
          } else if (me.FocusInput == false && this[nameRef] == "") {
            me.$refs[nameRef].$el.querySelector("input").focus();
            me.FocusInput = true;
          }
        });
      }
    },
     /**
     * Hàm bấm nút clone
     * @Author nmquang 19-12-2021
     */
    cloneAccountObject(id) {
      this.updateAccountObject(id); // lay du lieu

      this.getNewCode(); // lay code

      this.isshow = true; // mo form
    },
     /**
     * Hàm show thông tin nhà cung cấp
     * @Author nmquang 19-12-2021
     */
    async showAccountObject() {
      await this.updateAccountObject(this.focusId);
      this.isshow = true;
      this.disableMode = true;
    },
    /**
     * Xử lý sự kiện khi ấn nút sửa
     * @Author nmquang 19-12-2021
     */
    updateAccountObject(id) {
      var me = this;
      axios
        .get(`${Resource.AMIS_SERVICE_URL}/AccountObject/${id}`)
        .then((res) => {
          if (
            me.formMode == Enum.FormMode.Edit ||
            me.formMode == Enum.FormMode.Show
          ) {
            me.accountObjectCode = res.data.data.accountObjectCode;
          }
          me.taxCode = res.data.data.taxCode;
          me.accountObjectName = res.data.data.accountObjectName;
          me.phone = res.data.data.phone;
          me.webSite = res.data.data.website;
          if (res.data.data.accountObjectGroupListId.split(",")) {
            me.accountObjectGroupListId =
              res.data.data.accountObjectGroupListId.split(",");
          }
          me.listAccountGroupIdString = "";
          me.accountObjectGroupListId.forEach((item) => {
            me.listAccountGroupIdString += item + ",";
          });
          me.listAccountGroupIdString = me.listAccountGroupIdString.slice(
            0,
            this.listAccountGroupIdString.length - 1
          );
          me.address = res.data.data.address;
          me.employeeId = res.data.data.employeeId;
          me.prefix = res.data.data.prefix;
          me.einvoiceContactName = res.data.data.einvoiceContactName;
          me.einvoiceContactMobile = res.data.data.einvoiceContactMobile;
          me.einvoiceContactEmail = res.data.data.einvoiceContactEmail;
          me.legalRepresentative = res.data.data.legalRepresentative;
          me.paymentTermId = res.data.data.paymentTermId;
          me.maximizeDebtAmount = res.data.data.maximizeDebtAmount;
          me.dueTime = String(res.data.data.dueTime);
          me.payAccount = res.data.data.payAccount;
          me.accountObjectBankAccount = JSON.parse(
            res.data.data.accountObjectBankAccountJson
          );
          me.accountObjectShippingAddress = JSON.parse(
            res.data.data.accountObjectShippingAddressJson
          );
          me.country = res.data.data.country;
          me.provinceOrCity = res.data.data.provinceOrCity;
          me.district = res.data.data.district;
          me.wardOrCommune = res.data.data.wardOrCommune;
          me.description = res.data.data.description;

          me.editLocation = false;
          me.GetDataAccountObject();
      me.accountObjectString = JSON.stringify(me.tempAccountObject);
        });
      
    },
    /**
     * Hàm gọi Api lấy code mới
     * @Author nmquang 19-12-2021
     */
    async getNewCode() {
      try {
        await axios
          .get(`${Resource.AMIS_SERVICE_URL}/AccountObject/NewCode`)
          .then((res) => {
            this.accountObjectCode = res.data.data;
          });
      } catch (error) {
        new Toast(
          this.$parent.$refs.toast,
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },

    /**
     * Check Email
     * @Author nmquang 19-12-2021
     
     */
    errorEmail(email) {
      // const emailReg =
      //   /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      // if (emailReg.test(String(email).toLowerCase())) {
      //   return true;
      // }
      if (email != "" && email.indexOf("@") == -1) {
        return true;
      }
      return false;
    },
     /**
     * Hàm validate dữ liệu
     * @Author nmquang 19-12-2021
     */
    checkValidate() {
      var me = this;
      // Viền đỏ Input nếu không nhập và Focus
      me.ValidateMode = true;
      var valid = false; //// fixx
      // Hiển thị MessageBox cảnh báo:
      if (me.accountObjectCode == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.AccountObjectCodeIsEmpty;
      } else if (me.accountObjectName == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.AccountObjectNameIsEmpty; 
      } //else if(this.errorEmail(me.email)) {
      else if (
        me.einvoiceContactEmail != "" &&
        String(me.einvoiceContactEmail).indexOf("@") == -1
      ) {
        valid = true;
        me.MesageBoxText =  Resource["VN"].Warning.EmailError; 
      }
      if (valid) {
        me.showMsg = true;
        me.MessageBoxType = "error";
        return false;
      }
      return true;
    },
   /**
     * Hàm lấy dữ liệu phục vụ cho việc lưu trữ
     * @Author nmquang 19-12-2021
     */
    GetDataAccountObject() {
      let me = this;
      me.tempAccountObject = {
        //     accountObjectId:
        taxCode: me.taxCode,
        accountObjectCode: me.accountObjectCode,
        accountObjectName: me.accountObjectName,
        phone: me.phone,
        webSite: me.webSite,
        accountObjectGroupListId: me.listAccountGroupIdString,
        address: me.address,

        employeeId: me.employeeId,
        prefix: me.prefix,
        einvoiceContactName: me.einvoiceContactName,
        einvoiceContactMobile: me.einvoiceContactMobile,
        einvoiceContactEmail: me.einvoiceContactEmail,
        legalRepresentative: me.legalRepresentative,
        paymentTermId: me.paymentTermId,
        maximizeDebtAmount: me.maximizeDebtAmount,
        dueTime: parseInt(me.dueTime),
        payAccount: me.payAccount,
        accountObjectBankAccount: me.accountObjectBankAccount,
        accountObjectShippingAddress: me.accountObjectShippingAddress,
        country: me.country,
        provinceOrCity: me.provinceOrCity,
        district: me.district,
        wardOrCommune: me.wardOrCommune,
        description: me.description,
      };
    },
     /**
     * Hàm làm mới dữ liệu lưu trữ
     * @Author nmquang 19-12-2021
     */
    async RefreshAccountObject() {
      let me = this;
      me.accountObjectId = "";
      me.taxCode = "";
      me.accountObjectCode = "";
      me.accountObjectName = "";
      me.phone = "";
      me.webSite = "";
      me.accountObjectGroupListId = [];
      me.address = "";
      me.employeeId = null;
      me.prefix = "";
      me.einvoiceContactName = "";
      me.einvoiceContactMobile = "";
      me.einvoiceContactEmail = "";
      me.legalRepresentative = "";
      me.paymentTermId = null;
      me.maximizeDebtAmount = "";
      me.dueTime = "0";
      me.payAccount = "";
      me.accountObjectBankAccount = [
        {
          bank_account: "",
          bank_branch_name: "",
          bank_name: "",
          bank_province_or_city: "",
        },
      ];
      me.accountObjectShippingAddress = [{ address: "" }];
      me.country = "";
      me.provinceOrCity = "";
      me.district = "";
      me.wardOrCommune = "";
      me.description = "";
    },
  },
  computed: {},
  beforeDestroy() {
    /**
     * Hủy sự kiện của các phím khi đóng form
     * @Author nmquang 27-01-2022
     */
    document.removeEventListener("keydown", this.handleKeyUpList);
  },
  created() {
     /**
     * Thêm sự kiện cho các phìm tắt khi mở form
     * @Author nmquang 27-01-2022
     */
    document.addEventListener("keydown", this.handleKeyUpList);
    /**
     * Bắt sự kiện FormMode khi mở form thực hiện lấy các dữ liệu tương ứng
     * @Author nmquang 27-01-2022
     */
    if (this.formMode == Enum.FormMode.Add) {
      this.RefreshAccountObject();
      this.handleBtnAdd();
      this.isshow = true;
    } else if (this.formMode == Enum.FormMode.Edit) {
      this.updateAccountObject(this.focusId);
      this.isshow = true;
    } else if (this.formMode == Enum.FormMode.Clone) {
      this.cloneAccountObject(this.focusId);
    } else if (this.formMode == Enum.FormMode.Show) {
      this.showAccountObject();
    }
  },
  
  watch: {
    /**
     * Hàm bắt sự kiện khi danh sách nhà cung cấp thay đổi
     * @Author nmquang 27-1-2021
     */
    accountObjectGroupListId(value) {
      this.listAccountGroupIdString = "";
      value.forEach((item) => {
        this.listAccountGroupIdString += item + ",";
      });
      this.listAccountGroupIdString = this.listAccountGroupIdString.slice(
        0,
        this.listAccountGroupIdString.length - 1
      );
    },
     /**
     * Hàm bắt sự kiện khi tab trong form Detail thay đổi
     * @Author nmquang 27-1-2021
     */
    slotTab(value) {
      if (value == 1) {
        let temp = this.$refs.prefix.$el.querySelector("input");
        setTimeout(function () {
          temp.focus();
        });
      } else if (value == 2) {
        let temp = this.$refs.paymentTermId.$el.querySelector("input");
        setTimeout(function () {
          temp.focus();
        });
      } else if (value == 3) {
        let temp =
          this.$refs.accountObjectBankAccount.$el.querySelector("input");
        if (temp) {
          setTimeout(function () {
            temp.focus();
          });
        }
      } else if (value == 4) {
        let temp =
          this.$refs.accountObjectShippingAddress.$el.querySelector("input");
        if (temp) {
          setTimeout(function () {
            temp.focus();
          });
        }
      } else if (value == 5) {
        let temp = this.$refs.description;
        setTimeout(function () {
          temp.focus();
        });
      }
    },
     /**
     * Hàm bắt sự kiện khi địa chỉ thay đổi
     * @Author nmquang 27-1-2021
     */
    address(value) {
      if (this.coppyAddres == true) {
        this.accountObjectShippingAddress = [{ address: value }];
      }
    },
     /**
     * Hàm bắt sự kiện khi check vào ô giống địa chỉ nhà cung cấp
     * @Author nmquang 27-1-2021
     */
    coppyAddres(value) {
      if (value == true) {
        this.accountObjectShippingAddress = [{ address: this.address }];
      }
    },
     /**
     * Hàm bắt sự kiện khi dữ liệu country thay đổi
     * @Author nmquang 27-1-2021
     */
    country(value) {
      this.provinceOrCityList = Location["VN"].cities.filter(function (item) {
        if (item.parent_id == value) {
          return true;
        }
        return false;
      });
      if (this.editLocation &&  value != "") {
        this.provinceOrCity = "";
        this.district = "";
        this.wardOrCommune = "";
      }
    },
     /**
     * Hàm bắt sự kiện khi thành phố thay đổi
     * @Author nmquang 27-1-2021
     */
    provinceOrCity(value) {
      let me = this;
      me.districtList = Location["VN"].district.filter(function (item) {
        if (item.parent_id == value) {
          return true;
        }
        return false;
      });
      if (this.editLocation &&  value != "") {
        this.district = "";
        this.wardOrCommune = "";
      }
 
    },
    /**
     * Hàm bắt sự kiện khi quận huyện thay đổi
     * @Author nmquang 27-1-2021
     */
    district(value) {

      this.wardOrCommuneList = Location["VN"].wardOrCommune.filter(function (
        item
      ) {
        if (item.parent_id == value) {
          return true;
        }
        return false;
      });
      if (this.editLocation &&  value != "") {
        this.wardOrCommune = "";
      }
    },


  },
};
</script>

<style scope>
@import url("../../assets/styles/base/popup.css");

.m-popup-footer .divide {
  margin: 32px 0 20px 0;
  border-top: 1px solid #e0e0e0;
}

.custom-padding {
  padding-left: 19.5px;
  padding-right: 19.5px;
}
.m-popup-tittle {
  display: flex;
  align-items: center;
}
.al_center {
  align-items: center;
  display: flex;
}
.m-popup-close {
  display: flex;
}
.m-popup-close .m-popup-btn {
  cursor: pointer;
  height: 24px;
  width: 24px;
}
.m-popup-close .m-popup-btn + .m-popup-btn {
  margin-left: 6px;
}
.m-popup-content {
  background-color: #fff;
  border-radius: 5px;
  animation: scale 0.5s;
}

.m-popup__header {
  display: flex;
  justify-content: space-between;
}
.m-popup__header .m-popup-tittle {
  padding: 20px 12px 20px 32px;
  font-size: 24px;
  color: #111;
  font-weight: 700;
  font-family: Notosans-Regular;
}
.m-popup__header .m-popup-close {
  padding: 12px;
}
.m-popup {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  animation: fadein 0.5s;
  z-index: 999;
}
.m-employee-view {
  background-color: #f4f5f8;
  flex: 1;
  max-height: calc(100vh - 48px);
  overflow: auto;
  position: relative;
}
.m-employee-view .m-employee-view__header {
  padding-left: 20px;
  padding-top: 24px;
  padding-bottom: 11px;
  padding-right: 30px;
  display: flex;
  justify-content: space-between;
}
.m-employee-view .m-employee-view__header .m-employee-view__tittle {
  color: #111;
  font-family: Notosans-Regular;
  font-size: 24px;
  font-weight: 700;
}
.m-employee-view .m-employee-view__body {
  background-clip: border-box;
  box-sizing: border-box;
  margin-left: 20px;
  margin-top: 20px;
  margin-right: 30px;
  background-color: #fff;
}
.m-employee-view .m-employee-view__footer {
  position: sticky;
  bottom: 0;
  padding: 12px;
  background-color: #fff;
  margin-left: 20px;
  margin-right: 30px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  z-index: 2;
}
.m-employee-view .m-employee-view__footer .right-pagination {
  display: flex;
  align-items: center;
}
.btn-24 {
  width: 24px;
  height: 24px;
  padding: 0 6px;
  box-sizing: content-box;
  cursor: pointer;
}

.w-1\/2 {
  width: 50%;
}
.w-2\/5 {
  width: 40% !important;
}
.p-r-6 {
  padding-right: 6px;
}
.w-3\/5 {
  width: 60%;
}
.p-r-26 {
  padding-right: 26px;
}
.w-1\/4 {
  width: 25%;
}
.m-text-field input[type="date"] {
  outline: none;
  font-size: 13px;
  border-radius: 2px;
  border: 1px solid #babec5;
  height: var(--input-height);
  padding-left: 10px;
  padding-right: 12px;
  box-sizing: border-box;
  width: 100%;
  text-transform: uppercase;
  font-style: italic;
  font-family: "Notosans-Regular" !important;
}

.m-text-tab {
  outline: none;
  border-radius: 3px;
  border: 1px solid #babec5;
  font-size: 13px;
  height: 191px;
  width: calc(100% - 16px);
  margin: 11px 8px;
  padding: 10px;
}

.m-text-tabhover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-text-tab:focus {
  border: 1px solid #2ca01c;
}

.m-text-field input[type="date"]:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-text-field input[type="date"]:focus {
  border: 1px solid #2ca01c;
}
.m-popup-footer-btn {
  border: 1px solid #8d9096;
  color: #111;
  border-radius: 3px;
  height: 34px;
  padding: 6px 16px;
}
.m-popup-footer-btn + .m-popup-footer-btn {
  margin-left: 6px;
}
.m-input-error {
  border: 1px solid red !important;
}
.loading {
  position: fixed;
  background: rgba(255, 255, 255, 0.2);
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}

@keyframes rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(1turn);
  }
}
@keyframes fadein {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@keyframes scale {
  from {
    transform: scale(0.7);
  }
  to {
    transform: scale(1);
  }
}
</style>