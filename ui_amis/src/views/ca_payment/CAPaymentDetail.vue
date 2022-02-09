<template>
  <div class="m-popup" style="overflow-y: scroll; align-items: normal">
    <div
      class="m-popup-content"
      style="width: 100%; height: fit-content; min-height: 100%"
    >
      <div class="m-popup__header" style="background: #f4f5f8">
        <div class="dp-flex">
          <div class="m-popup-tittle">Phiếu chi {{ caPaymentNo }}</div>
          <div class="al_center">
            <BaseCombobox
              :noneThead="true"
              :fields="[{ name: 'value' }]"
              :datas="listCaPaymentMode"
              v-model="caPaymentMode"
              :textShow="'value'"
              :id="'id'"
            />
          </div>
        </div>

        <div class="m-popup-close">
          <div class="dp-flex" style="padding-right: 12px">
            <div class="mi-tour btn-24"></div>
            <a href="">Hướng dẫn</a>
          </div>
          <div class="m-popup-btn setting_list"></div>
          <div class="m-popup-btn help" @click="handleHelpPopup"></div>
          <div class="m-popup-btn close" @click="handleClosePopup"></div>
        </div>
      </div>
      <div class="body-layout">
        <div class="body-top">
          <div class="m-popup-body p-b-0">
            <div class="grid__row p-b-0" style="position: relative">
              <div class="grid__row w-3/4 p-b-0">
                <div
                  class="grid__row p-b-0"
                  style="position: relative; width: 100%"
                >
                  <div class="grid__row w-4/5 p-b-0">
                    <div
                      class="grid__row p-r-12 p-b-0"
                      style="border-right: 1px solid #c7c7c7"
                    >
                      <BaseCombobox
                        class="grid__row w-3/7 p-r-12"
                        :label="'Đối tượng'"
                        :fields="fieldsAccountObject"
                        :datas="accountObjects"
                        :width="'920px'"
                        :textShow="'accountObjectName'"
                        :id="'accountObjectId'"
                        v-model="accountObjectId"
                        :disable="disableMode"
                      />
                      <TextField
                        :label="'Người nhận'"
                        class="grid__row w-4/7"
                        v-model="receiver"
                        :disable="disableMode"
                      />
                      <TextField
                        :label="'Địa chỉ'"
                        class="grid__row"
                        v-model="address"
                        :disable="disableMode"
                      />
                      <TextField
                        :label="'Lý do chi'"
                        class="grid__row p-b-0"
                        v-model="resion"
                        :disable="disableMode"
                      />
                    </div>
                  </div>
                  <div
                    class="grid__row w-1/5 p-l-12"
                    style="position: absolute; top: 0; right: 0"
                  >
                    <BaseDateTimeField
                      class="grid__row"
                      :label="'Ngày hạch toán'"
                      v-model="postedDate"
                      :disable="disableMode"
                      ref="postedDate"
                      :checkInput="checkInput('postedDate', postedDate)"
                      :errorText="resource.VN.Warning.CaPaymentDateError"
                    />
                    <BaseDateTimeField
                      class="grid__row"
                      :label="'Ngày phiếu chi'"
                      v-model="caPaymentDate"
                      :disable="disableMode"
                    />
                    <TextField
                      :label="'Số phiếu chi'"
                      class="grid__row"
                      v-model="caPaymentNo"
                      :required="true"
                      :disable="disableMode"
                      ref="caPaymentNo"
                      :checkInput="checkInput('caPaymentNo', caPaymentNo)"
                      :errorText="resource.VN.Warning.caPaymentNoIsEmpty"
                    />
                  </div>
                </div>

                <div class="grid__row w-4/5 p-r-6 p-t-12">
                  <BaseCombobox
                    class="grid__row w-3/7 p-r-12"
                    :label="'Nhân viên'"
                    :fields="fieldEmployee"
                    :datas="dataEmployees"
                    :textShow="'fullName'"
                    :id="'employeeId'"
                    :width="'600px'"
                    v-model="employeeId"
                    :disable="disableMode"
                  />
                  <div class="grid__row w-4/7 p-r-6" style="position: relative">
                    <TextField
                      :label="'Kèm theo'"
                      class="w-2/5 p-r-6"
                      :disable="disableMode"
                    />
                    <div
                      class="w-3/5 p-r-6"
                      style="position: absolute; top: 25px; right: 3px"
                    >
                      chứng từ gốc
                    </div>
                  </div>
                  <div class="grid__row p-b-0">
                    <div>Tham chiếu</div>
                    <div class="btn-24">...</div>
                  </div>
                </div>
              </div>
              <div
                style="
                  position: absolute;
                  right: 0px;
                  top: 0px;
                  height: 60px;
                  display: inline-grid;
                "
              >
                <span>Tổng tiền</span>
                <div style="font-size: 36px; font-weight: 700; text-align: end">
                  {{ String(totalAmount) | formatMoney }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="body-bottom">
          <div class="m-popup-body">
            <div class="grid__row p-t-16">
              <a href="">Hạch toán</a>
              <div class="grid__row p-b-0">
                <div class="p-r-6">Loại tiền</div>
                <BaseCombobox
                  style="width: 100px"
                  :disable="disableMode"
                  :noneThead="true"
                  :fields="[{ name: 'value' }]"
                  :datas="fieldMonney"
                  v-model="monneyMode"
                  :textShow="'value'"
                  :id="'id'"
                />
              </div>
            </div>
            <!-- <table-bank /> -->
            <div>
              <BaseTableFocus
                ref="tableDetail"
                :fields="fieldCAaPaymentDetail"
                v-model="dataDetail"
                :disable="disableMode"
                @selectComboboxTable="selectComboboxTable"
                :monney="totalAmount"
                @blur="blurTable"
                @removeRow="removeRowTable"
                @removeAllRow="removeAllRow"
                @checkInput="checkInputTable"
              />

              <div class="ms-upload" style="padding: 0 8px">
                <div class="ms-upload-title">
                  <div style="display: flex">
                    <div class="mi-attach mi-20"></div>
                    <span style="font-weight: bold">Dinh kem</span>
                  </div>

                  <span style="margin-left: 10px">Dung luong toi da 5Mb</span>
                </div>
                <div>
                  <input
                    disabled="true"
                    type="text"
                    style="height: 48px"
                    placeholder="            Kéo/thả tệp vào đây hoặc bấm vào đây"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div
        class="m-popup_footer"
        style="
          position: fixed;
          width: 100%;
          bottom: 0px;
          height: 50px;
          background: #38393d;
        "
      >
        <div class="grid__row p-b-0" style="padding: 8px 16px; direction: rtl">
          <div>
            <Button
              class="m-popup-footer-btn"
              style="color: #fff; border: none; background: #2ca01c"
              @handleClick="handleBtnSaveAndAdd"
              >Cất và Thêm</Button
            >
            <Button
              class="m-popup-footer-btn"
              style="
                color: #fff;
                border: none;
                background: #38393d;
                margin-right: 12px;
              "
              @handleClick="handleBtnSave"
              >Cất</Button
            >
          </div>
          <div>
            <Button
              class="m-popup-footer-btn"
              style="color: #fff; border: none; background: #38393d"
              @handleClick="distroyPopup"
              >Hủy</Button
            >
          </div>
        </div>
      </div>
    </div>

    <MessageBoxCloseForm
      v-if="showMsgCloseForm"
      :width="'444px'"
      :text="MesageBoxText"
      @handleCanclemMessage="showMsgCloseForm = false"
      @handleNoConfirmMessage="handleNoConfirmMessage"
      @handleConfirmMessage="handleBtnSave"
    />

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
    <div ref="toast" id="toast"></div>
  </div>
</template>
<script>
import axios from "axios";
import Button from "../../components/base/BaseButton.vue";
import TextField from "../../components/base/BaseTextField.vue";
import BaseCombobox from "../../components/account_object/ComboboxAccObj.vue";
import BaseDateTimeField from "../../components/base/BaseDateTimeField.vue";
import BaseTableFocus from "../../components/ca_payment/BaseTableFocus.vue";
import Resource from "../../assets/js/capayment/resource";
import Toast from "../../assets/js/toast";
import MessageBox from "../../components/base/BaseMessageBox.vue";
import MessageBoxCloseForm from "../../components/custom/MessageBoxCloseForm.vue";
import Enum from "../../assets/js/enum";

export default {
  components: {
    Button,
    TextField,
    BaseCombobox,
    BaseDateTimeField,
    //   TableBank,
    BaseTableFocus,
    MessageBox,
    MessageBoxCloseForm,
  },
  props: {
    accountObjects: Array,
    dataEmployees: Array,
    focusId: String,
    formMode: {
      default: 0,
      type: Number,
    },
  },
  data() {
    let fieldsAccountObject = [
      {
        text: "MÃ NHÀ CUNG CẤP",
        name: "accountObjectCode",
        width: "140px",
      },
      {
        text: "TÊN NHÀ CUNG CẤP",
        name: "accountObjectName",
        width: "250px",
      },
      {
        text: "MÃ SỐ THUẾ",
        name: "taxCode",
        width: "100px",
      },
      {
        text: "ĐỊA CHỈ",
        name: "addres",
        width: "300px",
      },

      {
        text: "Điện thoại",
        name: "phone",
        width: "100px",
      },
    ];
    let fieldCreditAccount = [
      {
        name: "creditAccountId",
        text: "số tài khoản",
        width: "150px",
      },
      {
        name: "creditAccountName",
        text: "tên tài khoản",
        width: "150px",
      },
    ];

    let dataCreditAccount = [
      {
        creditAccountId: "1111",
        creditAccountName: "VND",
      },
      {
        creditAccountId: "1122",
        creditAccountName: "Ngoại tệ",
      },
      {
        creditAccountId: "1121",
        creditAccountName: "Tiền Việt Nam",
      },
      {
        creditAccountId: "1212",
        creditAccountName: "Tiền tệ",
      },
    ];

    let fieldDebitAccount = [
      {
        name: "debitAccountId",
        text: "số tài khoản",
        width: "150px",
      },
      {
        name: "debitAccountName",
        text: "tên tài khoản",
        width: "150px",
      },
    ];

    let dataDebitAccount = [
      {
        debitAccountId: "564",
        debitAccountName: "Tài khoản 1",
      },
      {
        debitAccountId: "234",
        debitAccountName: "Tài khoản 2",
      },
      {
        debitAccountId: "356",
        debitAccountName: "Tài khoản 3",
      },
      {
        debitAccountId: "456",
        debitAccountName: "Tài khoản 4",
      },
    ];
    return {
      fieldsAccountObject: fieldsAccountObject,
      fieldCreditAccount: fieldCreditAccount,
      dataCreditAccount: dataCreditAccount,
      fieldDebitAccount: fieldDebitAccount,
      dataDebitAccount: dataDebitAccount,

      fieldEmployee: [
        {
          text: "MÃ nhân viên",
          name: "employeeCode",
          width: "140px",
        },
        {
          text: "TÊN nhân viên",
          name: "fullName",
          width: "250px",
        },
        {
          text: "Đơn vị",
          name: "departmentName",
          width: "150px",
        },
        {
          text: "Số điện thoại",
          name: "phoneNumber",
          width: "120px",
        },
      ],

      listCaPaymentMode: [
        {
          id: 1,
          value: "1. Trả tiền nhà cung cấp",
        },
        {
          id: 2,
          value: "2. Tạm ứng cho nhân viên",
        },
        {
          id: 3,
          value: "3. Chi mua ngoài có hóa đơn",
        },
        {
          id: 4,
          value: "4. Trả lương nhân viên",
        },
        {
          id: 5,
          value: "5. Chuyển tiên cho chi nhánh khác",
        },
        {
          id: 6,
          value: "6. Gửi tiền vào ngân hàng",
        },
        {
          id: 7,
          value: "7. Chi khác",
        },
      ],

      fieldMonney: [
        {
          id: 1,
          value: "VNĐ",
        },
        {
          id: 2,
          value: "USD",
        },
      ],

      fieldCAaPaymentDetail: [
        {
          name: "decription",
          text: "Diễn giải",
          width: "450px",
        },
        {
          name: "creditAccountId",
          text: "TK có",
          type: "combobox",
          width: "170px",
          dataCombobox: dataCreditAccount,
          fieldCombobox: fieldCreditAccount,
          widthCombobox: "300px",
          idCombobox: "creditAccountId",
          textShowCombobox: "creditAccountId",
        },
        {
          name: "debitAccountId",
          text: "Tk nợ",
          type: "combobox",
          width: "170px",
          dataCombobox: dataDebitAccount,
          fieldCombobox: fieldDebitAccount,
          widthCombobox: "300px",
          idCombobox: "debitAccountId",
          textShowCombobox: "debitAccountId",
        },
        {
          name: "amount",
          text: "số tiền",
          type: "monney",
          width: "200px",
        },
        {
          name: "accountObjectId",
          text: "đối tượng",
          type: "combobox",
          width: "230px",
          dataCombobox: this.accountObjects,
          fieldCombobox: fieldsAccountObject,
          widthCombobox: "900px",
          idCombobox: "accountObjectId",
          textShowCombobox: "accountObjectCode",
          locationCombobox: "-100%",
        },
        {
          name: "accountObjectName",
          text: "tên đối tượng",
          type: "read",
          getData: "accountObjectId",
          width: "300px",
          // customDisplayMethod: function (value, obj) {
          //   return "anh" + value;
          //   fieldsAccountObject.forEach((temp) => {
          //     if (value == temp.accountObjectId) {
          //       obj.accountObjectName = temp.accountObjectName;
          //     }
          //   });
          //   return obj.accountObjectName;
          // },
        },
      ],
      totalAmountString: "",
      // du lieu doi tuong

      caPaymentId: "", // id phiếu chi
      caPaymentNo: "", // số phiếu chi
      accountObjectId: null, // id nhà cung cấp
      receiver: "", // người nhận
      address: "", // địa chỉ ncc
      resion: "Chi tiền cho", // lý do
      employeeId: null, // mã nhân viên
      caPaymentDate: null, // ngày phiếu chi
      postedDate: null, // ngày hạch toán
      totalAmount: 0, // tổng tiền
      file: "",
      //  createdDate: "2022-01-20T14:30:44",
      dataDetail: [],
      /// du lieu câpyment
      tempMasterDetailCaPayment: [],
      caPaymentString: "",

      /// check ngày chứng từ ngày hạch toán
      checkDate: false,
      checkResion: ["true"],
      ///
      disableMode: false,
      ValidateMode: false,
      ///
      MesageBoxText: "",
      ///
      showMsg: false,
      showMsgCloseForm: false,
      //
      caPaymentMode: "7",
      monneyMode: "1",

      saveAdd: false,

      resource: Resource,

      inputElement: null,
    };
  },

  created() {
    /**
     * Thêm sự kiện cho các phìm tắt khi mở form
     * @Author nmquang 27-01-2022
     */
    document.addEventListener("keydown", this.handleKeyUpList);
    // this.CallApiGetCaPaymentDetailById("6733eace-bc24-4d83-9621-5fdb70790fc6");
  },
  beforeDestroy() {
    /**
     * Hủy sự kiện của các phím khi đóng form
     * @Author nmquang 27-01-2022
     */
    document.removeEventListener("keydown", this.handleKeyUpList);
  },
  mounted() {
    /**
     * Bắt sự kiện FormMode khi mở form thực hiện lấy các dữ liệu tương ứng
     * @Author nmquang 27-01-2022
     */

    if (this.formMode == Enum.FormMode.Add) {
      this.disableMode = false;
      this.newDataCaPayment();
    } else if (this.formMode == Enum.FormMode.Show) {
      this.CallApiGetCaPaymentDetailById(this.focusId);
      this.disableMode = true;
    } else if (this.formMode == Enum.FormMode.Clone) {
      this.CallApiGetCaPaymentDetailById(this.focusId);
      this.getNewCode();
      this.disableMode = false;
    }
  },
  methods: {
    /**
     * Hàm xử lý sự kiện khi bấm nút cất và thêm
     * @Author nmquang 27-01-2022
     */
    async handleBtnSaveAndAdd() {
      var me = this;
      me.saveAdd = true;
      try {
        let saveSuccess = await me.handleBtnSave();
        if (saveSuccess) {
          console.log("thanh cong");
          me.disableMode = false;
          await this.$emit("saveAndAdd");
        } else {
          me.ValidateMode = true;
        }
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },

    /**
     * Hàm xử lý sự kiện validate trong table
     * @Author nmquang 27-01-2022
     */
    checkInputTable(nameRef, value) {
      let me = this;
      let valid = false;
      if (
        nameRef == "debitAccountId" &&
        me.ValidateMode == true &&
        value == null
      ) {
        valid = true;
      } else {
        valid = false;
      }
      return valid;
    },
    /**
     * Hàm xử lý sự kiện validate trong form
     * @Author nmquang 27-01-2022
     */
    checkInput(nameRef, value) {
      var me = this;
      let valid = false;
      if (nameRef == "postedDate") {
        if (me.ValidateMode == true && me.caPaymentDate > me.postedDate) {
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
     * Hàm xử lý sự kiện khi bấm hủy message
     * @Author nmquang 27-01-2022
     */
    handleNoConfirmMessage() {
      let me = this;
      me.showMsgCloseForm = false;
      me.distroyPopup();
      this.refreshdata();
    },

    /**
     * Hàm xử lý sự kiện khi click khỏi table
     * @Author nmquang 27-01-2022
     */
    removeAllRow() {
      this.calculatorAmount();
      this.checkDecription();
    },
    removeRowTable() {
      this.calculatorAmount();
      this.checkDecription();
    },
    blurTable() {
      this.calculatorAmount();
      this.checkDecription();
    },
    /**
     * Hàm xử lý sự kiện khi dữ liệu datadetail thay đổi và check dữ liệu diễn giải
     * @Author nmquang 27-01-2022
     */
    checkDecription() {
      this.checkResion = [];
      this.dataDetail.forEach((data) => {
        if (data.decription == this.resion) {
          this.checkResion.push("true");
        } else {
          this.checkResion.push("false");
        }
      });
    },
    /**
     * Hàm xử lý sự kiện tính toán tổng tiền trong table
     * @Author nmquang 27-01-2022
     */
    calculatorAmount() {
      let totalAmount = 0;
      this.dataDetail.forEach((data) => {
        if (data.amount) {
          totalAmount += Number(data.amount);
        }
      });
      this.totalAmount = totalAmount;
    },
    /**
     * Hàm xử lý sự kiện khi close Popup
     * @Author nmquang 27-01-2022
     */
    distroyPopup() {
      this.$emit("handleClosePopup");
    },
    /**
     * Hàm tạo data mới Capayment
     * @Author nmquang 27-01-2022
     */
    async newDataCaPayment() {
      let me = this;
      me.resion = "Chi tiền cho";

      await this.getNewCode();
      this.postedDate = this.getNewDate();
      this.caPaymentDate = this.getNewDate();

      me.dataDetail = [
        {
          accountObjectId: null,
          accountObjectName: null,
          accountObjectCode: null,
          amount: "0",
          creditAccountId: "1111",
          debitAccountId: null,
          decription: "Chi tiền cho",
        },
      ];
      this.dataSave();
      this.caPaymentString = JSON.stringify(this.tempMasterDetailCaPayment);
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
      }

      if (e.keyCode == 27) {
        this.$parent.$refs.btnAdd.$el.querySelector("button").blur();
        e.preventDefault(); // hủy event mặc định
        e.stopPropagation(); // tránh liên quan đến event khác
        this.handleClosePopup();
      }
      // //Shift N để mở form thêm mới.
      // if(e.shiftKey &&  e.keyCode == 78)
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
     * Hàm xử lý sự kiện khi bấm bàn phím(chỉ nhập số)
     * @Author nmquang 27-01-2022
     */
    onlyNumber($event) {
      //console.log($event.keyCode); //keyCodes value
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
        // 46 is dot
        $event.preventDefault();
      }
    },

    /**
     * Hàm xử lý sự kiện khi chấp nhận thông báo
     * @Author nmquang 27-01-2022
     */
    async handleConfirmMessage() {
      let me = this;
      if (me.formMode == Enum.FormMode.Add) {
        await me.getNewCode();
        //await me.handleBtnSave();
        me.dataSave();
        try {
          await me.callApiCreate(me.tempMasterDetailCaPayment);
          new Toast(
            this.$parent.$refs.toast,
            Resource["VN"].ToastMessage.AddSuccess
          );
          me.showMsg = false;
          me.distroyPopup();
          me.reLoadData();
        } catch (error) {
          new Toast(
            this.$parent.$refs.toast,
            Resource["VN"].ToastMessage.Error,
            "error"
          );
        }
      }
    },
    /**
     * Hàm xử lý sự kiện khi chọn combobox trong table
     * @Author nmquang 27-01-2022
     */
    selectComboboxTable(data, field, fieldData) {
      if (field.name == "accountObjectId") {
        fieldData.accountObjectName = data.accountObjectName;
        fieldData.accountObjectCode = data.accountObjectCode;
      }
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
      me.dataSave();
      let checkDataToString = JSON.stringify(me.tempMasterDetailCaPayment);
      if (me.formMode != Enum.FormMode.Show && checkDataToString != me.caPaymentString) {
        me.showMsgCloseForm = true;
      } else {
        me.distroyPopup();
        me.reLoadData();
      }

      this.disableMode = false;
    },
    /**
     * Hàm gọi Api lấy dữ liệu nhà cung cấp theo id
     * @Author nmquang 27-01-2022
     */
    async CallApiGetAccountObjectById(id) {
      let me = this;
      await axios
        .get(`${Resource.AMIS_SERVICE_URL}/AccountObject/${id}`)
        .then((res) => {
          if (res.data.data) {
            if (res.data.data.accountObjectId == id) {
              me.receiver = res.data.data.accountObjectName;
              me.address = res.data.data.address;
              me.resion = `Chi tiền cho ${res.data.data.accountObjectName}`;
              if (
                !me.dataDetail[0].accountObjectId &&
                me.dataDetail.length == 1
              ) {
                me.dataDetail = [
                  {
                    accountObjectId: res.data.data.accountObjectId,
                    accountObjectName: res.data.data.accountObjectName,
                    accountObjectCode: res.data.data.accountObjectCode,
                    amount: "0",
                    creditAccountId: "1111",
                    debitAccountId: null,
                    decription: me.resion,
                  },
                ];
              }

              console.log(me.dataDetail);
            }
          }
        });
    },
    /**
     * Hàm gọi Api lấy dữ liệu phiếu chi theo Id
     * @Author nmquang 27-01-2022
     */
    async CallApiGetCaPaymentDetailById(id) {
      let me = this;
      await axios
        .get(
          `${Resource.AMIS_SERVICE_URL}/CaPayment/GetMasterDetailById?id=${id}`
        )
        .then((res) => {
          if (res.data.data) {
            me.caPaymentId = res.data.data.master.caPaymentId;
            if (me.formMode == Enum.FormMode.Show) {
              me.caPaymentNo = res.data.data.master.caPaymentNo;
            }
            me.accountObjectId = res.data.data.master.accountObjectId;
            me.receiver = res.data.data.master.receiver;
            me.address = res.data.data.master.address;
            me.resion = res.data.data.master.resion;
            me.employeeId = res.data.data.master.employeeId;
            me.caPaymentDate = me.convertDate(
              res.data.data.master.caPaymentDate
            );
            me.postedDate = me.convertDate(res.data.data.master.postedDate);
            me.totalAmount = res.data.data.master.totalAmount;
            me.dataDetail = res.data.data.detail;
          }
        });
    },
    /**
     * Hàm focus vào ô nhập liệu bị  validate
     * @Author nmquang 27-01-2022
     */
    doFocus() {
      var me = this;
      if (me.formMode != null) {
        let ListFocus = ["caPaymentNo"];
        ListFocus.forEach((nameRef) => {
          if (me.FocusInput == false && this[nameRef] == "") {
            me.$refs[nameRef].$el.querySelector("input").focus();
            me.FocusInput = true;
          }
        });
      }
      if (me.inputElement) {
        setTimeout(() => {
          me.inputElement.focus();
        }, 0);
      }
    },

    /**
     * Hàm check validate
     * @Author nmquang 19-12-2021
     */
    checkValidate() {
      var me = this;
      // Viền đỏ Input nếu không nhập và Focus
      me.ValidateMode = true;
      var valid = false;
      // Hiển thị MessageBox cảnh báo:
      let checkDebitId = false;
      me.dataDetail.forEach((data, index) => {
        if (!data.debitAccountId && checkDebitId == false) {
          console.log(index);
          me.$refs.tableDetail.$refs[
            index + "-debitAccountId"
          ][0].setErrorValidation(true);
          me.$refs.tableDetail.select = index;
          me.inputElement =
            me.$refs.tableDetail.$refs[
              index + "-debitAccountId"
            ][0].$el.querySelector("input");
          valid = true;
          me.MesageBoxText = Resource["VN"].Warning.DebitAccountIsEmpty;
          checkDebitId = true;
        }
      });
      if (me.caPaymentNo == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.caPaymentNoIsEmpty;
      } else if (me.caPaymentDate > me.postedDate) {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.CaPaymentDateError;
      }

      if (valid) {
        me.showMsg = true;
        me.MessageBoxType = "error";
        return false;
      }
      return true;
    },
    /**
     * Hàm lấy dữ liệu phục vụ lưu trữ
     * @Author nmquang 27-01-2022
     */
    dataSave() {
      let me = this;
      let caPayment = {
        caPaymentNo: me.caPaymentNo,
        accountObjectId: me.accountObjectId,
        receiver: me.receiver,
        address: me.address,
        resion: me.resion,
        employeeId: me.employeeId,
        caPaymentDate: me.caPaymentDate,
        postedDate: me.postedDate,
        totalAmount: me.totalAmount,
      };
      let caPaymentDetailList = me.dataDetail;
      console.log(caPaymentDetailList);
      me.tempMasterDetailCaPayment = {
        entityState: 1,
        caPayment,
        caPaymentDetailList,
      };
    },
    /**
     * Hàm làm mới dữ liệu
     * @Author nmquang 27-01-2022
     */
    refreshdata() {
      let me = this;
      let caPayment = {
        caPaymentNo: "",
        accountObjectId: null,
        receiver: "",
        address: "",
        resion: "",
        employeeId: null,
        caPaymentDate: "",
        postedDate: "",
        totalAmount: 0,
      };
      let caPaymentDetailList = [];
      console.log(caPaymentDetailList);
      me.tempMasterDetailCaPayment = {
        entityState: 1,
        caPayment,
        caPaymentDetailList,
      };
    },

    /**
     * Hàm xử lý sự kiện khi bấm nút lưu
     * @Author nmquang 27-01-2022
     */
    async handleBtnSave() {
      var me = this;
      me.showMsgCloseForm = false;
      // Validate dữ liệu:
      let validate = me.checkValidate();
      me.FocusInput = false;

      //-----------------
      if (validate) {
        me.dataSave();
        console.log(me.tempMasterDetailCaPayment);
        if (
          me.formMode == Enum.FormMode.Add ||
          me.formMode == Enum.FormMode.Clone
        ) {
          let isAvailable = await me.checkAvailableMasterDetailCode();
          if (isAvailable) {
            me.showMsg = true;
            me.MesageBoxText = Resource["VN"].GetCaPaymentNoConfirm(
              me.caPaymentNo
            );
            //me.MesageBoxText = `Số chứng từ <${me.caPaymentNo}> đã tồn tại. Nếu không tìm thấy số chứng từ <${me.caPaymentNo}>. Bạn có muốn chương trình tự động tăng số chứng từ không?`;
            me.MessageBoxType = "warning";
          } else {
            // Thực hiện thêm nhân viên
            try {
              await me.callApiCreate(me.tempMasterDetailCaPayment);
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
        }
        return true;
      }
      return false;
    },

    /**
     * Hàm gọi Api lấy code mới
     * @Author nmquang 19-12-2021
     */
    async getNewCode() {
      try {
        await axios
          .get(`${Resource.AMIS_SERVICE_URL}/CaPayment/NewNo`)
          .then((res) => {
            this.caPaymentNo = res.data.data;
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
     * Hàm làm mới dữ liệu đổ lên table
     * @Author nmquang 19-12-2021
     */
    reLoadData() {
      this.$emit("reLoadData");
    },
    /**
     * Hàm gọi APi thêm mới
     * @Author nmquang 19-12-2021
     */
    async callApiCreate(caPayment) {
      try {
        const res = await axios.post(
          `${Resource.AMIS_SERVICE_URL}/CaPayment/insertMasterDetail`,
          caPayment
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
    async checkAvailableMasterDetailCode() {
      var me = this;
      let isAvailable = false;
      await axios
        .get(
          `${Resource.AMIS_SERVICE_URL}/CaPayment/GetMasterDetailByCode?Code=${me.caPaymentNo}`
        )
        .then((res) => {
          if (res.data.data.master) {
            if (me.formMode == Enum.FormMode.Edit) {
              if (res.data.data.master.caPaymentNo == me.focusId) {
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
    /**
     * Hàm lấy và định dạng ngày hiện tại
     * @Author nmquang 19-12-2021
     */
    getNewDate() {
      var result = null;
      let date = new Date();
      let day =
        date.getDate() < 10 ? `0${date.getDate()}` : `${date.getDate()}`;
      let month =
        date.getMonth() + 1 < 10
          ? `0${date.getMonth() + 1}`
          : `${date.getMonth() + 1}`;
      result = `${date.getFullYear()}-${month}-${day}`;

      return result;
    },
    /**
     * Hàm chuyển đổi kiểu dữ liệu Date sang String
     * @Author nmquang 19-12-2021
     */
    convertDate(dateString) {
      var result = null;
      if (dateString) {
        let date = new Date(dateString);
        let day =
          date.getDate() < 10 ? `0${date.getDate()}` : `${date.getDate()}`;
        let month =
          date.getMonth() + 1 < 10
            ? `0${date.getMonth() + 1}`
            : `${date.getMonth() + 1}`;
        result = `${date.getFullYear()}-${month}-${day}`;
      }
      return result;
    },
  },

  filters: {
    // Filter tiền
    formatMoney: function (value) {
      if (value) {
        return Number(value)
          .toFixed(0)
          .replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
      }
      return "";
    },
  },
  // computed: {
  //   totalAmount: function(){
  //     val.forEach((el) => {
  //       if (el.amount) {
  //        return totalAmount += Number(el.amount);
  //       }
  //     });
  //   }
  // },

  watch: {
    /**
     * Hàm bắt sự kiện khi dữ liệu detail thay đổi
     * @Author nmquang 27-1-2021
     */
    dataDetail() {
      this.checkDecription();
      this.calculatorAmount();
    },
    /**
     * Hàm bắt sự kiện khi resion(lý do chi) thay đổi
     * @Author nmquang 27-1-2021
     */
    resion(value) {
      let index = 0;
      this.checkResion.forEach((check) => {
        if (check === "true") {
          if (this.dataDetail[index].decription) {
            this.dataDetail[index].decription = value;
          }
        }
        index++;
      });
    },
    /**
     * Hàm bắt sự kiện khi nhà cung cấp thay đổi
     * @Author nmquang 27-1-2021
     */
    accountObjectId(value) {
      if (this.formMode == Enum.FormMode.Add) {
        this.CallApiGetAccountObjectById(value);
      }
    },
    /**
     * Hàm bắt sự kiện khi ngày hạch toán thay đổi
     * @Author nmquang 27-1-2021
     */
    postedDate(value) {
      if (this.checkDate) {
        this.caPaymentDate = value;
      }
      if (value == this.caPaymentDate) {
        this.checkDate = true;
      } else {
        this.checkDate = false;
      }
    },
    /**
     * Hàm bắt sự kiện khi ngày phiếu chi thay đổi
     * @Author nmquang 27-1-2021
     */
    caPaymentDate(value) {
      if (value == this.postedDate) {
        this.checkDate = true;
      } else {
        this.checkDate = false;
      }
    },
  },
};
</script>

<style scope>
@import url("../../assets/styles/base/popup.css");

.body-layout {
  height: calc(100vh - 110px);
}
.m-popup-footer .divide {
  margin: 32px 0 20px 0;
  border-top: 1px solid #e0e0e0;
}
.m-popup-body {
  padding-left: 32px;
  padding-right: 32px;
  padding-bottom: 20px;
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
  margin-right: 6px;
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
.p-b-0 {
  padding-bottom: 0;
}
.p-r-12 {
  padding-right: 12px;
}
.p-t-12 {
  padding-top: 12px;
}
.p-t-16 {
  padding-top: 16px;
}
.p-l-12 {
  padding-left: 12px;
}
.w-3\/5 {
  width: 60%;
}
.w-4\/5 {
  width: 80%;
}
.w-3\/7 {
  width: 43%;
}
.w-4\/7 {
  width: 57%;
}
.p-r-26 {
  padding-right: 26px;
}
.w-1\/4 {
  width: 25%;
}
.w-1\/5 {
  width: 20%;
}
.w-3\/4 {
  width: 75%;
}
.ms-upload {
  height: 75px;
  width: 340px;
}
.ms-upload .ms-upload-title {
  margin-bottom: 6px;
  display: flex;
}

.ms-upload input[type="text"] {
  outline: none;
  font-size: 13px;
  border-radius: 2px;
  border: 1px solid #babec5;
  height: var(--input-height);
  padding-left: 10px;
  padding-right: 32px;
  box-sizing: border-box;
  width: 100%;
}

.ms-upload input[type="text"]:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.ms-upload input[type="text"] ::placeholder {
  font-style: italic;
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
.dp-flex {
  display: flex;
}
.body-top {
  background: #f4f5f8;
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