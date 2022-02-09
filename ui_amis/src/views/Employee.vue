<template>
  <div class="m-employee-view">
    <div v-if="Loading" class="loading">
      <div class="loading__icon"></div>
    </div>
    <div class="m-employee-view__header">
      <div class="m-employee-view__tittle">Nhân viên</div>
      <Button
        :color="'#2ca01c'"
        style="border-radius: 2px"
        @handleClick="handleBtnAdd"
        >Thêm mới nhân viên</Button
      >
    </div>
    <div class="m-employee-view__body">
      <div class="grid__row" style="padding: 0 16px">
        <div class="grid__row" style="padding-bottom: 0px !important">
          <DropBox @multiDelete="DeleteAll" :isShow="showBtnDeleteAll" />
        </div>
        <div class="grid__row" style="justify-content: right; padding: 16px 0">
          <TextInput
            :hintText="'Tìm theo mã, tên nhân viên'"
            :iconClass="'search'"
            v-model="SearchText"
          />

          <div class="btn-24 refresh-ico" @click="handleBtnRefresh"></div>
          <a 
            >
            <div class="btn-24 excel" @click="exportExcel"></div
          ></a>
        </div>
      </div>
      <Table
        :fields="fields"
        :data="employees"
        @handleDeleteButtonClick="handleTableDelete"
        @handleEditButtonClick="updateEmployee"
        @insertEmployeeIdToListId="insertEmployeeIdToListId"
        @deleteEmployeeIdToListId="deleteEmployeeIdToListId"
        @handleCloneBtnClick="handleCloneBtnClick"
      ></Table>
    </div>
    <div class="m-employee-view__footer">
      <div class="left-pagination">
        <div class="total-record">
          Tổng số: <b>{{ TotalRecord }}</b> bản ghi
        </div>
      </div>
      <div class="right-pagination">
        <ComboboxPanigaion
          :ListItem="[
            '10 bản ghi trên trang',
            '20 bản ghi trên trang',
            '30 bản ghi trên trang',
            '50 bản ghi trên trang',
            '100 bản ghi trên trang',
          ]"
          v-if="true"
          @handleSelect="changeRecordNumber"
        />
        <PageNumberPanigation
          :totalPage="TotalPage"
          @handleSelectPanigation="handleSelectPanigation"
        />
      </div>
    </div>
    <Popup
      v-if="showPopup"
      @handleClosePopup="handleClosePopup"
      @handleHelpPopup="Help"
      :width="'900px'"
    >
      <template slot="body">
        <div class="grid__row" style="align-items: start">
          <div class="w-1/2 p-r-26">
            <div class="grid__row">
              <TextField
                class="w-2/5 p-r-6"
                :label="'Mã'"
                :required="true"
                v-model="EmployeeCode"
                :checkInput="checkInput('EmployeeCode', EmployeeCode)"
                :errorText="resource.VN.Warning.EmployeeCodeIsEmpty"
                ref="EmployeeCode"
              />
              <TextField
                v-model="FullName"
                class="w-3/5"
                :label="'Tên'"
                :required="true"
                :checkInput="checkInput('FullName', FullName)"
                :errorText="resource.VN.Warning.FullNameIsEmpty"
                ref="FullName"
              />
            </div>
            <Combobox
              class="grid__row"
              :label="'Đơn vị'"
              :required="true"
              :departments="Departments"
              v-model="DepartmentId"
              :checkInput="checkInput('DepartmentId', DepartmentId)"
              :errorText="resource.VN.Warning.DepartmentIsEmpty"
              ref="DepartmentId"
              
            />
            <TextField
              class="grid__row"
              v-model="PositionName"
              :label="'Chức danh'"
              :required="false"
            />
          </div>
          <div class="w-1/2">
            <div class="grid__row">
              <!--Ngày sinh-->
              <div class="m-text-field w-2/5 p-r-6">
                <div class="m-text-field__label">
                  <div class="m-text-tittle">Ngày sinh</div>
                </div>
                <input v-model="DateOfBirth" style="width: 100%" type="date" />
              </div>
              <!---->
              <RadioGroup
                v-model="Gender"
                class="w-3/5"
                :label="'Giới tính'"
                :items="listGender"
              />
            </div>
            <div class="grid__row">
              <TextField
                v-model="IdentityNumber"
                class="w-3/5 p-r-6"
                :label="'Số CMND'"
                :required="false"
              />
              <!--Ngày cấp CMND-->
              <div class="m-text-field w-2/5">
                <div class="m-text-field__label">
                  <div class="m-text-tittle">Ngày cấp</div>
                </div>
                <input
                  ref="IdentityDate"
                 v-model="IdentityDate" style="width: 100%" type="date" />
              </div>
              <!---->
            </div>
            <TextField
              v-model="IdentityPlace"
              class="grid__row"
              :label="'Nơi cấp'"
            />
          </div>
        </div>
        <div class="grid__row">
          <TextField v-model="Address" :label="'Địa chỉ'" :required="false" />
        </div>
        <div class="grid__row" style="justify-content: start">
          <TextField
            v-model="PhoneNumber"
            class="w-1/4 p-r-6"
            :label="'ĐT di động'"
            :required="false"
          />
          <TextField
            v-model="LandlinePhoneNumber"
            class="w-1/4 p-r-6"
            :label="'ĐT cố định'"
            :required="false"
          />
          <TextField
            v-model="Email"
            class="w-1/4 p-r-6"
            :label="'Email'"
            :required="false"
          />
        </div>
        <div class="grid__row" style="justify-content: start">
          <TextField
            v-model="BankAccount"
            class="w-1/4 p-r-6"
            :label="'Tài khoản ngân hàng'"
            :required="false"
          />
          <TextField
            v-model="BankName"
            class="w-1/4 p-r-6"
            :label="'Tên ngân hàng'"
            :required="false"
          />
          <TextField
            v-model="BankBranch"
            class="w-1/4 p-r-6"
            :label="'Chi nhánh'"
            :required="false"
          />
        </div>
      </template>
      <template slot="footer">
        <div class="grid__row">
          <div class="w-1/2">
            <Button
              class="m-popup-footer-btn"
              :color="'transparent'"
              @handleClick="showPopup = false"
              >Hủy</Button
            >
          </div>
          <div>
            <Button
              class="m-popup-footer-btn"
              :color="'transparent'"
              @handleClick="handleBtnSave"
              >Cất</Button
            >
            <Button
              class="m-popup-footer-btn"
              :color="'#2ca01c'"
              style="color: #fff; border: none"
              @handleClick="handleBtnSaveAndAdd"
              >Cất và Thêm</Button
            >
          </div>
        </div>
      </template>
    </Popup>
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
import Resource from "../assets/js/resource";
import axios from "axios";
import Button from "../components/base/BaseButton.vue";
import TextInput from "../components/base/BaseTextInput.vue";
import Table from "../components/base/BaseCustomTable.vue";
import Popup from "../components/custom/Popup.vue";
import TextField from "../components/base/BaseTextField.vue";
import RadioGroup from "../components/base/BaseRadioGroup.vue";
import Combobox from "../components/custom/ComboboxCustom.vue";
import MessageBox from "../components/base/BaseMessageBox.vue";
import Toast from "../assets/js/toast";
import ComboboxPanigaion from "../components/custom/ComboboxPanigation.vue";
import PageNumberPanigation from "../components/custom/PageNumberPanigation.vue";
import DropBox from "../components/custom/DropBox.vue";
import MessageBoxCloseForm from "../components/custom/MessageBoxCloseForm.vue";

export default {
  components: {
    Button,
    TextInput,
    Table,
    Popup,
    TextField,
    RadioGroup,
    Combobox,
    MessageBox,
    ComboboxPanigaion,
    PageNumberPanigation,
    DropBox,
    MessageBoxCloseForm,
  },
  created() {
    var me = this;
    me.loadData();
  },
  data() {
    return {
      // Màn hình load
      Loading: true,
      // Phân trang:
      PageNumber: 1,
      RecordNumber: 10,
      TotalRecord: null,
      TotalPage: 3,
      // Tìm kiếm:
      SearchText: "",
      // Validate Mode;
      ValidateMode: false,
      FocusInput: false,
      // MessageBox:
      MesageBoxText: "",
      MessageBoxType: "warning",

      Validate: true,
      /* Thông tin cần dùng v-model */
      EmployeeCode: "", // Mã nhân viên
      FullName: "", // Họ và Tên
      DateOfBirth: null, // Ngày sinh
      Gender: null, // Giới tính
      DepartmentId: "", // Đơn vị
      IdentityNumber: "", // Số CMND
      IdentityDate: null, // Ngày cấp CMND
      IdentityPlace: "", // Nơi cấp CMND
      PositionName: "", // Chức danh
      Address: "", // Địa chỉ
      PhoneNumber: "", // SĐT Di Động
      Email: "", // Email
      BankAccount: "", // Tài khoản ngân hàng
      LandlinePhoneNumber: "", // Số Đt cố định
      BankName: "", // Tên ngân hàng
      BankBranch: "", // chi nhanh tài khoản ngân hàng
      /*---------------------*/
      showMsg: false, // show Message
      showMsgCloseForm: false, //
      EmployeeEditId: null,
      // Lưu lại Employee cần xóa
      EmployeeCodeDelete: null,
      EmployeeIdDelete: null,
      ListEmployeeIdDelete: [],
      // ----------------------------
      resource: Resource,
      timemout: null,
      FormMode: null,
      Departments: [],
      deletes: false,
      showPopup: false,
      showBtnDeleteAll: true,
      employees: [],
      tempEmployee: {},
      employeeToString: null,
      checkEmployeeToString: null,
      listGender: [
        {
          id: "0",
          name: Resource["VN"].Gender.Male,
        },
        {
          id: "1",
          name: Resource["VN"].Gender.Female,
        },
        {
          id: "2",
          name: Resource["VN"].Gender.Other,
        },
      ],
      fields: [
        {
          text: Resource["VN"].titleEmployee.employeeCode,
          name: "employeeCode",
          width: "120px",
        },
        {
          text: Resource["VN"].titleEmployee.fullName,
          name: "fullName",
        },
        {
          text: Resource["VN"].titleEmployee.gender,
          name: "gender",
          type: "gender",
          width: "80px",
        },
        {
          text: Resource["VN"].titleEmployee.dateOfBirth,
          name: "dateOfBirth",
          type: "date",
          width: "90px",
        },
        {
          text: Resource["VN"].titleEmployee.identityNumber,
          name: "identityNumber",
          width: "88px",
        },
        {
          text: Resource["VN"].titleEmployee.positionName,
          name: "positionName",
          width: "110px",
        },
        {
          text: Resource["VN"].titleEmployee.departmentName,
          name: "departmentName",
        },
        {
          text: Resource["VN"].titleEmployee.bankAccount,
          name: "bankAccount",
          width: "110px",
        },
        {
          text: Resource["VN"].titleEmployee.bankName,
          name: "bankName",
          width: "120px",
        },
        {
          text: Resource["VN"].titleEmployee.bankBranch,
          name: "bankBranch",
          width: "185px",
        },
      ],
    };
  },
  methods: {
    /**
     * Hàm xử lý cất và thêm
     * @Author nmquang(22/12/2021)
     */
    async handleBtnSaveAndAdd() {
      var me = this;
      try {
        let saveSuccess = await me.handleBtnSave();
        if (saveSuccess) {
          await me.handleBtnAdd();
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
    handleClosePopup(){
      let me = this;
      me.GetDataEmployee()
      me.checkEmployeeToString = JSON.stringify(me.tempEmployee)
      if(me.checkEmployeeToString != me.employeeToString){
        me.showMsgCloseForm = true
      }else{
        me.showPopup = false
      }

    },
    /**
     * Sự kiện khi thay đổi RecordNumber
     * @Author nmquang 19-12-2021
     */
    changeRecordNumber(pageRecord) {
      var me = this;
      me.RecordNumber = pageRecord;
      me.CallApiEmployeePaging();
    },
    /**
     * Xử lý khi nhấn chọn trang
     *@Author nmquang 19-12-2021
     */
    handleSelectPanigation(page) {
      var me = this;
      me.PageNumber = page;
      this.CallApiEmployeePaging();
    },
    /**
     * Hàm xử lý sự kiện Refresh
     *@Author nmquang 19-12-2021
     */
    async handleBtnRefresh() {
      var me = this;
      await me.CallApiEmployeePaging();
      new Toast(
        document.getElementById("toast"),
        Resource["VN"].ToastMessage.RefreshSucess
      );
    },
    /**
     * Xử lý sự kiện khi ấn nút sửa
     * @Author nmquang 19-12-2021
     */
    async updateEmployee(id) {
      var me = this;
      me.EmployeeEditId = id;
      me.FormMode = Resource.FormMode.Edit;
      await axios
        .get(`${Resource.AMIS_SERVICE_URL}/Employee/${id}`)
        .then((res) => {
          me.EmployeeCode = res.data.data.employeeCode;
          me.FullName = res.data.data.fullName;
          me.DateOfBirth = me.convertDate(res.data.data.dateOfBirth);
          me.Gender = res.data.data.gender;
          me.DepartmentId = res.data.data.departmentId;
          me.IdentityNumber = res.data.data.identityNumber;
          me.IdentityDate = me.convertDate(res.data.data.identityDate);
          me.IdentityPlace = res.data.data.identityPlace;
          me.PositionName = res.data.data.positionName;
          me.Address = res.data.data.address;
          me.PhoneNumber = res.data.data.phoneNumber;
          me.Email = res.data.data.email;
          me.BankAccount = res.data.data.bankAccount;
          me.LandlinePhoneNumber = res.data.data.landlinePhoneNumber;
          me.BankName = res.data.data.bankName;
          me.BankBranch = res.data.data.bankBranch;
          me.showPopup = true;
          me.GetDataEmployee();
           me.employeeToString =  JSON.stringify(me.tempEmployee)
        });
    },
    /**
     * Hàm xử lý khi bấm nút nhân bản
     * @Author nmquang 19-12-2021
     */
    async handleCloneBtnClick(id) {
      var me = this;
      me.FormMode = Resource.FormMode.Add;
      await this.getNewCode();
      await axios
        .get(`${Resource.AMIS_SERVICE_URL}/Employee/${id}`)
        .then((res) => {
          me.FullName = res.data.data.fullName;
          me.DateOfBirth = me.convertDate(res.data.data.dateOfBirth);
          me.Gender = res.data.data.gender;
          me.DepartmentId = res.data.data.departmentId;
          me.IdentityNumber = res.data.data.identityNumber;
          me.IdentityDate = me.convertDate(res.data.data.identityDate);
          me.IdentityPlace = res.data.data.identityPlace;
          me.PositionName = res.data.data.positionName;
          me.Address = res.data.data.address;
          me.PhoneNumber = res.data.data.phoneNumber;
          me.Email = res.data.data.email;
          me.BankAccount = res.data.data.bankAccount;
          me.LandlinePhoneNumber = res.data.data.landlinePhoneNumber;
          me.BankName = res.data.data.bankName;
          me.BankBranch = res.data.data.bankBranch;
          me.showPopup = true;
        });
      
    },
    /**
     * Hàm xử lý khi ấn nút xóa 1 dòng
     * @Author nmquang 19-12-2021
     */
    handleTableDelete(employee) {
      var me = this;
      me.EmployeeIdDelete = employee.employeeId;
      me.EmployeeCodeDelete = employee.employeeCode;
      me.showMsg = true;
      me.MesageBoxText = Resource["VN"].DeleteConfirm(me.EmployeeCodeDelete);
      me.MessageBoxType = "warning";
    },
    handleNoConfirmMessage() {
      let me = this;
      me.showMsgCloseForm = false;
      me.showPopup = false;
      me.RefreshEmployee();
    },
    /**
     * Hàm xử lý sự kiện khi ấn xác nhận MessageBox
     * @Author nmquang 19-12-2021
     */
    async handleConfirmMessage() {
      var me = this;
      try {
        if (me.deletes) {
          var listId = "";
          me.ListEmployeeIdDelete.forEach((item) => {
            listId += item + ",";
          });
          listId = listId.slice(0, listId.length - 1);
          me.ListEmployeeIdDelete = [];
          await me.CallApiDeleteAll(listId);
        } else {
          await me.callApiDelete(me.EmployeeIdDelete);
        }
        me.deletes = false;
        me.showMsg = false;
        me.CallApiEmployeePaging();
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.DeleteSuccess
        );
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm Load dữ liệu từ API
     * @Author nmquang 19-12-2021
     */
    async loadData() {
      var me = this;
      me.CallApiEmployeePaging();
      await axios
        .get(`${Resource.AMIS_SERVICE_URL}/Department`)
        .then((res) => {
          me.Departments = res.data.data;
        })
        .catch();
      me.Loading = false;
    },
    /**
     * Hàm xử lý exporExcel
     * @Author nmquang 19-12-2021
     */
    async exportExcel(event) {
      let me = this
      event.target.parentElement.href = `${Resource.AMIS_SERVICE_URL}/Employee/ExportExcel?searchText=${me.SearchText}`
      event.target.parentElement.click();
    },
    /**
     * hàm xóa nhiều
     * @Author nmquang 19-12-2021
     */
    DeleteAll() {
      var me = this;
      var lenght = me.ListEmployeeIdDelete.length;
      this.showMsg = true;
      if (lenght > 0) { 
        me.MesageBoxText = Resource["VN"].Question.DeleteAll;
        me.MessageBoxType = "warning";
        me.deletes = true;
      } else {
        me.MesageBoxText = Resource["VN"].Error.ListDeleteNull;
        me.MessageBoxType = "error";
      }
    },
    /**
     * Hàm xử lý sự kiện nhấn nút thêm nhân viên mới
     * @Author nmquang 19-12-2021
     */
    async handleBtnAdd() {
      var me = this;
      me.showPopup = true;
      me.ValidateMode = false;  
      me.FormMode = Resource.FormMode.Add;
      me.RefreshEmployee();
      await this.getNewCode();
      me.showPopup = true;
      me.FocusInput = false;
      me.GetDataEmployee()
      me.employeeToString =  JSON.stringify(me.tempEmployee)
    },
    RefreshEmployee() {
      let me= this
      me.EmployeeCode = "";
      me.FullName = "";
      me.DateOfBirth = null;
      me.Gender = null;
      me.DepartmentId = "";
      me.IdentityNumber = "";
      me.IdentityDate = null;
      me.IdentityPlace = "";
      me.PositionName = "";
      me.Address = "";
      me.PhoneNumber = "";
      me.Email = "";
      me.BankAccount = "";
      me.LandlinePhoneNumber = "";
      me.BankName = "";
      me.BankBranch = "";
    },
    GetDataEmployee(){
      let me = this;
      me.tempEmployee = {
          employeeCode: me.EmployeeCode,
          fullName: me.FullName,
          dateOfBirth: me.DateOfBirth,
          gender: me.Gender,
          departmentId: me.DepartmentId,
          identityNumber: me.IdentityNumber,
          identityDate: me.IdentityDate,
          identityPlace: me.IdentityPlace,
          positionName: me.PositionName,
          address: me.Address,
          phoneNumber: me.PhoneNumber,
          email: me.Email,
          bankAccount: me.BankAccount,
          landlinePhoneNumber: me.LandlinePhoneNumber,
          bankName: me.BankName,
          bankBranch: me.BankBranch,
        };
      
    },

    
    /**
     * Hàm check điều kiện và hiển thị Input báo đỏ
     * @Author nmquang 19-12-2021
     */
    checkInput(nameRef, value) {
      var me = this;
      let valid = false;
      if (me.ValidateMode == true && value == "") {
        valid = true;
      } else {
        valid = false;
      }
      return valid;
    },
    /**
     * Hàm Focus vào ô nhập liệu
     * @Author nmquang 19-12-2021
     */
    doFocus() {
      var me = this;
      if (me.FormMode != null) {
        let ListFocus = ["EmployeeCode", "FullName", "DepartmentId"];
        ListFocus.forEach((nameRef) => {
          if (me.FocusInput == false && this[nameRef] == "") {
            me.$refs[nameRef].$el.querySelector("input").focus();
            me.FocusInput = true;
          }
        });
      }
    },
    /**
     * Check Date
     * @Author nmquang 19-12-2021
     */
    errorDate(date) {
      if (date == null) return false;
      else {
        let dateInput = new Date(date);
        let now = new Date();
        if (dateInput.getFullYear() > now.getFullYear()) return true;
        else if (dateInput.getFullYear() == now.getFullYear()) {
          if (dateInput.getMonth() > now.getMonth()) return true;
          else if (dateInput.getMonth() == now.getMonth()) {
            if (dateInput.getDate() > now.getDate()) return true;
          }
        }
      }
      return false;
    },
    /**
     * Check Email
     * @Author nmquang 19-12-2021
     
     */
    errorEmail(email) {
      const emailReg =
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      if (emailReg.test(String(email).toLowerCase())) {
        return true;
      }
      return false;
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
      if (me.EmployeeCode == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.EmployeeCodeIsEmpty;
      } else if (me.FullName == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.FullNameIsEmpty;
      } else if (me.DepartmentId == "") {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.DepartmentIsEmpty;
      } else if (me.errorDate(me.DateOfBirth)) {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.DateOfBirthError;
      } else if (me.errorDate(me.IdentityDate)) {
        me.$refs.IdentityDate.$el.addClass('borderred')
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.IndentityDate;
      } else if (me.Email != "" && me.Email.indexOf("@") == -1) {
        valid = true;
        me.MesageBoxText = Resource["VN"].Warning.EmailError;
      }
      if (valid) {
        me.showMsg = true;
        me.MessageBoxType = "error";
        return false;
      }

      return true;
    },
    /**
     * check Trùng mã
     * @Author nmquang 19-12-2021
     */
    async checkAvailableEmployeeCode() {
      var me = this;
      let isAvailable = false;
      await axios
        .get(
          `${Resource.AMIS_SERVICE_URL}/Employee/GetByCode?EmployeeCode=${me.EmployeeCode}`
        )
        .then((res) => {
          if (res.data.data) {
            isAvailable = true;
          }
        });
      return isAvailable;
    },
    /**
     * Hàm gọi Api lấy code mới
     * @Author nmquang 19-12-2021
     */
    async getNewCode() {
      try {
        await axios
          .get(`${Resource.AMIS_SERVICE_URL}/Employee/NewCode`)
          .then((res) => {
            this.EmployeeCode = res.data.data;
          });
      } catch (error) {
         new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm gọi Api thêm mới
     * @Author nmquang 19-12-2021
     */
    async callApiCreate(employee) {
      try {
        const res = await axios.post(
          `${Resource.AMIS_SERVICE_URL}/Employee`,
          employee
        );
        return res;
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm gọi Api update
     * @Author nmquang 19-12-2021
     */
    async callApiUpdate(id, employee) {
      try {
        const res = await axios.put(
          `${Resource.AMIS_SERVICE_URL}/Employee/${id}`,
          employee
        );
        return res;
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm gọi Api xóa 1 nhân viên
     * @Author nmquang 19-12-2021
     */
    async callApiDelete(id) {
      try {
        await axios.delete(`${Resource.AMIS_SERVICE_URL}/Employee/${id}`);
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm gọi Api xóa nhiều nhân viên
     * @Author nmquang 19-12-2021
     */
    async CallApiDeleteAll(listId) {
      try {
        //let deleteAll = true;
        //nếu chọn xóa tất cả thì listId = 0000-0000-0000-000-0000
        await axios.delete(
          `${Resource.AMIS_SERVICE_URL}/Employee/DeleteMulti/${listId}`
        );
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm lấy dữ liệu tìm kiếm + paging
     * @Author nmquang 19-12-2021
     */
    CallApiEmployeePaging() {
      var me = this;
      me.Loading = true;
      try {
        axios
          .get(
            `${Resource.AMIS_SERVICE_URL}/Employee/Filter?searchText=${me.SearchText}&pageSize=${me.RecordNumber}&PageIndex=${me.PageNumber}`
          )
          .then((res) => {
            me.employees = res.data.data;
            me.TotalRecord = res.data.totalRecord;
            me.TotalPage =
              Math.floor(me.TotalRecord / me.RecordNumber) +
              (me.TotalRecord % me.RecordNumber == 0 ? 0 : 1);
            me.Loading = false;
          });
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm thêm Employee
     * @Author nmquang 19-12-2021
     */
    async handleBtnSave() {
      
      var me = this;
      me.showMsgCloseForm = false;
      // Validate dữ liệu:
      let validate = me.checkValidate();
      me.FocusInput = false;

      //-----------------
      if (validate) {
         me.GetDataEmployee()
        if (me.FormMode == Resource.FormMode.Add) {
          let isAvailable = await me.checkAvailableEmployeeCode();
          if (isAvailable) {
            me.showMsg = true;
            me.MesageBoxText = Resource["VN"].Warning.EmployeeCodeIsAvailable;
            me.MessageBoxType = "error";
          } else {
            // Thực hiện thêm nhân viên
            try {
              await me.callApiCreate(me.tempEmployee);
              new Toast(
                document.getElementById("toast"),
                Resource["VN"].ToastMessage.AddSuccess
              );
              me.showPopup = false;
              
              me.CallApiEmployeePaging();
            } catch (error) {
              new Toast(
                document.getElementById("toast"),
                Resource["VN"].ToastMessage.Error,
                "error"
              );
            }
          }
        } else if (me.FormMode == Resource.FormMode.Edit) {
          var isValid = false;
          await axios
            .get(
              `${Resource.AMIS_SERVICE_URL}/Employee/GetByCode?EmployeeCode=${me.EmployeeCode}`
            )
            .then((res) => {
              if (res.data.data) {
                if (res.data.data.employeeId == me.EmployeeEditId) {
                  isValid = false;
                } else {
                  isValid = true;
                }
              }
            });
          if (isValid) {
            me.showMsg = true;
            me.MesageBoxText = Resource["VN"].Warning.EmployeeCodeIsAvailable;
            me.MessageBoxType = "error";
          } else {
            try {
              me.tempEmployee.employeeId = me.EmployeeEditId;
              await me.callApiUpdate(me.EmployeeEditId, me.tempEmployee);
              new Toast(
                document.getElementById("toast"),
                Resource["VN"].ToastMessage.UpdateSuccess
              );
              me.showPopup = false;

              me.CallApiEmployeePaging();
            } catch (error) {
              new Toast(
                document.getElementById("toast"),
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
     * Hàm
     */
    async Help() {
      console.log("Help Popup Clicked");
    },
    /**
     * Hàm chuyển dữ liệu ngày tháng về dạng YYY-MMM-DDD
     * @Author nmquang 19-12-2021
     */
    // Convert Date:
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
    /**
     * Hàm thêm id vào danh sách  xóa
     * @Author nmquang 19-12-2021
     */
    insertEmployeeIdToListId(employeeId) {
      var me = this;
      var checkDuplicate = false;
      me.ListEmployeeIdDelete.filter(function (value) {
        if (value == employeeId) {
          checkDuplicate = true;
        }
      });
      if (!checkDuplicate) {
        me.ListEmployeeIdDelete.push(employeeId);

      }
    },
    /**
     * Hàm xóa id khỏi danh sách xóa
     * @Author nmquang 19-12-2021
     */
    deleteEmployeeIdToListId(employeeId) {
      var me = this;
      me.ListEmployeeIdDelete = me.ListEmployeeIdDelete.filter(function (
        value
      ) {
        // duyệt qua các phần tử của mảng lấy ra phần tử giống tham số truyền vào
        return value != employeeId;
      });
    },
  },
  watch: {
    /**
     * 
     */
    EmployeeToString(){
      if(this.employeeToString != null){
    this.showMsgCloseForm = true
      }
      
    },
    /**
     * bắt sự kiện tắt popup
     * @Author nmquang 19-12-2021
     */
    showPopup: function () {
      if (this.showPopup == false) {
        this.FormMode = null;
      }
    },
    /**
     * Hàm xử lý tìm kiếm
     * settimeout khi thay đổi keyword
     * @Author nmquang 19-12-2021
     */
    SearchText: function () {
      if (this.timemout) {
        clearTimeout(this.timemout);
      }
      this.timemout = setTimeout(() => {
        this.CallApiEmployeePaging();
        this.PageNumber = 1;
      }, 350);
    },
    /** gàn giá trị null nếu truyền vào giá trị bằng ""
     * @Author nmquang 19-12-2021
     */
    DateOfBirth: function (value) {
      if (value == "") this.DateOfBirth = null;
    },
    IndentityDate: function (value) {
      if (value == "") this.IndentityDate = null;
    },
  },
};
</script>

<style scoped>
@import url("../assets/styles/components/toast/toast.css");

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
.loading__icon {
  background: transparent url("../assets/loading.svg") no-repeat center;
  width: 50px;
  height: 50px;
  transition: all 0.3s ease;
  animation: rotate 1s ease infinite;
}
@keyframes rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(1turn);
  }
}
</style>
