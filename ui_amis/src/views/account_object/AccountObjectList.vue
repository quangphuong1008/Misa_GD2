<template>
  <div class="m-employee-view">
    <div v-if="Loading" class="loading">
      <div class="loading__icon"></div>
    </div>
    <div class="grid__row">
      <div class="m-employee-view__header">
        <div>
          <div class="m-employee-view__tittle">Danh sách nhà cung cấp</div>
          <div>Tất cả danh mục</div>
        </div>

        <div class="grid__row" style="justify-content: right">
          <div class="mi-tour btn-24"></div>
          <a href="">Hướng dẫn</a>
          <DropTool />

          <Button
            ref="btnAdd"
            :color="'#2ca01c'"
            style="border-radius: 2px"
            @handleBtnAdd="handleBtnAdd"
          ></Button>
        </div>
      </div>
    </div>
    <div class="overview">
      <div class="w-1/3 pd-t-8 over-view overdue">
        <div style="background: #ff7f2c; height: 100%; color: ">
          <div class="dued-debit-overview p-relative">
            <div class="num">
              <p>{{ 5000000 | formatMoney }}</p>
            </div>
            <div class="m-num-text">Nợ quá hạn</div>
          </div>
        </div>
      </div>
      <div class="w-1/3 pd-t-8 px-2 over-view total-liabilities">
        <div style="background: #b8bcc3; height: 100%">
          <div class="dued-debit-overview p-relative">
            <div class="num">
              <p>{{ 5000000 | formatMoney }}</p>
            </div>
            <div class="m-num-text">Nợ quá hạn</div>
          </div>
        </div>
      </div>
      <div class="w-1/3 pd-t-8 over-view">
        <div style="background: #74cb2f; height: 100%">
          <div class="dued-debit-overview p-relative">
            <div class="num">
              <p>{{ 5000000 | formatMoney }}</p>
            </div>
            <div class="m-num-text">Nợ quá hạn</div>
          </div>
        </div>
      </div>
    </div>
    <div class="m-employee-view__body">
      <div class="grid__row" style="padding: 0 16px">
        <div class="grid__row" style="padding-bottom: 0px !important">
          <DropBox @multiDelete="DeleteAll" :isShow="showBtnDeleteAll" />
          <div class="droptool">
            <div class="drop-box" style="width: 80px">
              <span>Lọc</span>
              <div
                class="icon-16 dropdown-black"
                :class="{ rotate: isShow }"
                v-on:click="isShow = !isShow"
              ></div>
            </div>
            <div class="drop-filter" v-if="isShow">
              <div class="padding-body-filter">
                <div class="grid__row" style="padding-bottom: 2px">
                  <div class="w-1/2 p-r-6">
                    <div class="grid__row">
                      <Combobox :label="'Loại'" :placeholder="'Tất cả'" />
                    </div>
                    <div class="grid__row">
                      <Combobox
                        :label="'Tình trạng công nợ'"
                        :placeholder="'Tất cả'"
                      />
                    </div>
                  </div>
                  <div class="w-1/2">
                    <div class="grid__row">
                      <Combobox
                        :label="'Nhóm'"
                        :datas="dataAccountObjecGroups"
                        :fields="fieldAccObjGroup"
                        :width="'400px'"
                        v-model="mAccountObjectGroupCode"
                        :id="'accountObjectGroupCode'"
                        :textShow="'accountObjectGroupCode'"
                      />
                    </div>
                    <div class="grid__row">
                      <Combobox :label="'Trạng thái'" :placeholder="'Tất cả'" />
                    </div>
                  </div>
                </div>
                <div class="grid__row" style="padding-bottom: 2px">
                  <Combobox
                    class="w-1/3 p-r-6"
                    :label="'Tỉnh/TP'"
                    :placeholder="'Tỉnh/Thành Phố'"
                    :noneThead="true"
                    :datas="provinceOrCityList"
                    :fields="[{ name: 'location_name' }]"
                    :id="'location_id'"
                    :textShow="'location_name'"
                    v-model="mProvinceOrCity"
                  />
                  <Combobox
                    class="w-1/3 p-r-6"
                    :noneThead="true"
                    :label="'Quận/Huyện'"
                    :placeholder="'Quận/Huyện'"
                    v-model="mDistrict"
                    :datas="districtList"
                    :fields="[{ name: 'location_name' }]"
                    :id="'location_id'"
                    :textShow="'location_name'"
                  />
                  <Combobox
                    class="w-1/3"
                    :noneThead="true"
                    :label="'Xã/Phường'"
                    :placeholder="'Xã/Phường'"
                    v-model="mWardOrCommune"
                    :fields="[{ name: 'location_name' }]"
                    :datas="wardOrCommuneList"
                    :id="'location_id'"
                    :textShow="'location_name'"
                  />
                </div>

                <div class="m-popup-footer" style="padding: 0px">
                  <div class="divide"></div>
                  <div class="grid__row" style="margin-bottom: 10px">
                    <div class="w-1/2">
                      <BaseButton
                        class="m-popup-footer-btn"
                        style="color: black; background: #ffffff"
                        @handleClick="refreshFilter()"
                        >Hủy</BaseButton
                      >
                    </div>
                    <div>
                      <BaseButton
                        class="m-popup-footer-btn"
                        style="
                          color: #fff;
                          border: none;
                          background: #2ca01c;
                          font-weight: 600;
                        "
                        @handleClick="handleBtnFiler"
                        >Lọc</BaseButton
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div style="width: 700px; justify-content: flex-start; display: flex">
            <div class="grid__row p-b-0">
              <div
                v-for="(item, index) in filterArray"
                :key="index"
                class="item-acc-group"
              >
                <div>
                  {{ item.name }}
                </div>
                <div
                  class="mi-16 mi-close"
                  @click="deleteItemFilter(index, item)"
                ></div>
              </div>
            </div>
          </div>
        </div>

        <div class="grid__row" style="justify-content: right; padding: 16px 0">
          <TextInput
            :hintText="'Tìm theo mã, tên nhà cung cấp'"
            :iconClass="'search'"
            v-model="SearchText"
          />

          <div class="btn-24 refresh-ico" @click="handleBtnRefresh"></div>
          <a> <div class="btn-24 excel" @click="exportExcel"></div></a>
          <div class="btn-24 setting_list"></div>
        </div>
      </div>
      <Table
        :fields="fields"
        :data="accountObjects"
        :btnText="'Lập CT mua hàng'"
        :type="'AccountObject'"
        :id="'accountObjectId'"
        v-model="focusId"
        @handleDeleteButtonClick="handleTableDelete"
        @handleEditButtonClick="handleEditBtnClick"
        @insertEmployeeIdToListId="insertEmployeeIdToListId"
        @deleteEmployeeIdToListId="deleteEmployeeIdToListId"
        @handleCloneBtnClick="handleCloneBtnClick"
        @handleShowButtonClick="handleShowBtnClick"
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
    <AccountObjectDetail
      v-if="showPopup"
      @handleClosePopup="handleClosePopup"
      :focusId="focusId"
      :formMode="FormMode"
      :dataAccountObjecGroups="dataAccountObjecGroups"
      :dataPaymentTerms="dataPaymentTerms"
      :dataEmployees="dataEmployees"
      @reLoadData="CallApiDataPaging"
      @saveAndAdd="saveAndAddMode"
    />

    <BaseMessageBox
      v-if="showMsg"
      @handleConfirmMessage="handleConfirmMessage"
      @handleCanclemMessage="
        deletes = false;
        showMsg = false;
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

import Resource from "../../assets/js/account_object/resource";
import Button from "../../components/account_object/ButtonAdd.vue";
import TextInput from "../../components/base/BaseTextInput.vue";
import Table from "../../components/account_object/BaseCustomTable.vue";
import ComboboxPanigaion from "../../components/custom/ComboboxPanigation.vue";
import PageNumberPanigation from "../../components/custom/PageNumberPanigation.vue";
import DropBox from "../../components/custom/DropBox.vue";
import AccountObjectDetail from "./AccountObjectDetail.vue";
import DropTool from "../../components/account_object/DropTool.vue";
import Combobox from "../../components/account_object/ComboboxAccObj.vue";
import BaseButton from "../../components/base/BaseButton.vue";
import Toast from "../../assets/js/toast";
import BaseMessageBox from "../../components/base/BaseMessageBox.vue";
import Location from "../../assets/js/location";
import Enum from "../../assets/js/enum";

export default {
  components: {
    Button,
    TextInput,
    Table,
    ComboboxPanigaion,
    PageNumberPanigation,
    DropBox,
    AccountObjectDetail,
    DropTool,
    Combobox,
    BaseButton,
    BaseMessageBox,
  },

  data() {
    return {
      isShow: false,
      // Màn hình load
      Loading: false,
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
      Datas: [
        {
          name: "quang",
        },
      ],
      Validate: true,
      /* Thông tin cần dùng v-model */
      /*---------------------*/
      showMsg: false, // show Message
      showMsgCloseForm: false, //
      EmployeeEditId: null,
      // Lưu lại id cần xóa
      accountObjectIdDelete: null,
      accountObjectCodeDelete: null,
      ListAccountObjectDelete: [],
      // ----------------------------
      resource: Resource,
      timemout: null,
      deletes: false,
      showPopup: false,
      showBtnDeleteAll: false,
      employees: [],
      tempEmployee: {},
      employeeToString: null,
      checkEmployeeToString: null,

      accountObjects: [],

      /// data
      dataAccountObjecGroups: [],
      dataPaymentTerms: [],
      dataEmployees: [],
      // id focus
      focusId: "",
      // data lọc dữ liệu

      mAccountObjectGroupCode: "",
      mProvinceOrCity: null,
      mDistrict: null,
      mWardOrCommune: null,
      //
      filterArray: [],
      /// location
      provinceOrCityList: [],
      districtList: [],
      wardOrCommuneList: [],
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
      fields: [
        {
          text: "MÃ NHÀ CUNG CẤP",
          name: "accountObjectCode",
        },
        {
          text: "TÊN NHÀ CUNG CẤP",
          name: "accountObjectName",
        },
        {
          text: "ĐỊA CHỈ",
          name: "address",
        },
        {
          text: "MÃ SỐ THUẾ",
          name: "taxCode",
        },
        {
          text: "SĐT",
          name: "phone",
        },
        {
          text: "CMTND",
          name: "phone",
        },
        {
          text: "Nhóm nhà CC",
          name: "accountObjectGroupListId",
        },
      ],
    };
  },

  /**
   * Hàm bắt sự kiện khi dữ liệu khi form được hình thành
   * @Author nmquang 27-1-2021
   */
  created() {
    this.CallApiDataPaging();
    this.CallApiDataAccountObjectGroup();
    this.CallApiDataPaymentTerm();
    this.CallApiDataEmployee();
    this.provinceOrCityList = Location["VN"].cities;
    console.log(this.provinceOrCityList);
  },

  filters: {
    // Filter tiền
    formatMoney: function (value) {
      //  var numeral = require("numeral");
      if (value) {
        return Number(value)
          .toFixed(0)
          .replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");

        // return numeral(monney).format("0,0");
      }
      return "";
    },
  },
  watch: {
    /**
     * Hàm bắt sự kiện khi dữ liệu tìm kiếm thay đổi
     * @Author nmquang 27-1-2021
     */
    SearchText: function () {
      if (this.timemout) {
        clearTimeout(this.timemout);
      }
      this.timemout = setTimeout(() => {
        this.CallApiDataPaging();
        this.PageNumber = 1;
      }, 350);
    },
    /**
     * Hàm bắt sự kiện khi dữ liệu tỉnh thành phố thay đổi
     * @Author nmquang 27-1-2021
     */
    mProvinceOrCity(value) {
      console.log("tp " + value);
      let me = this;
      me.districtList = Location["VN"].district.filter(function (item) {
        if (item.parent_id == value) {
          return true;
        }
        return false;
      });
      me.mDistrict = null;
      me.mWardOrCommune = null;
    },
    /**
     * Hàm bắt sự kiện khi dữ liệu quận huyện thay đổi
     * @Author nmquang 27-1-2021
     */
    mDistrict(value) {
      console.log("quận " + value);

      this.wardOrCommuneList = Location["VN"].wardOrCommune.filter(function (
        item
      ) {
        if (item.parent_id == value) {
          return true;
        }
        return false;
      });
      this.mWardOrCommune = null;
    },
  },
  methods: {
    /**
     * Hàm gọi api lấy dữ liệu tìm kiếm + paging
     * @Author nmquang 27-1-2021
     */
    CallApiDataPaging() {
      var me = this;
      let ListItemFilter = [
        "mAccountObjectGroupCode",
        "mProvinceOrCity",
        "mDistrict",
        "mWardOrCommune",
      ];
      ListItemFilter.forEach((item) => {
        if (me[item] == null) {
          me[item] = "";
        }
      });
      let filter = {
        m_AccountObjectGroupCode: me.mAccountObjectGroupCode,
        m_ProvinceOrCity: me.mProvinceOrCity,
        m_District: me.mDistrict,
        m_WardOrCommune: me.mWardOrCommune,
      };
      let dataFilter = {
        filter: filter,
        searchText: me.SearchText,
        pageSize: me.RecordNumber,
        pageIndex: me.PageNumber,
      };
      console.log(dataFilter);
      me.Loading = true;
      try {
        axios
          .post(
            //  `${Resource.AMIS_SERVICE_URL}/AccountObject/Filter?searchText=${me.SearchText}&pageSize=${me.RecordNumber}&PageIndex=${me.PageNumber}`
            `${Resource.AMIS_SERVICE_URL}/AccountObject/Filterr`,
            dataFilter
          )
          .then((res) => {
            me.accountObjects = res.data.data;
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
     * Hàm gọi api lấy dữ liệu nhóm nhà cung cấp
     * @Author nmquang 27-1-2021
     */
    CallApiDataAccountObjectGroup() {
      var me = this;
      me.Loading = true;
      try {
        axios
          .get(`${Resource.AMIS_SERVICE_URL}/AccountObjectGroup`)
          .then((res) => {
            me.dataAccountObjecGroups = res.data.data;
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
     * Hàm gọi api lấy dữ liệu
     * @Author nmquang 27-1-2021
     */
    CallApiDataPaymentTerm() {
      var me = this;
      me.Loading = true;
      try {
        axios.get(`${Resource.AMIS_SERVICE_URL}/PaymentTerm`).then((res) => {
          me.dataPaymentTerms = res.data.data;
          console.log(me.dataPaymentTerms);
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
     * Hàm gọi api lấy dữ liệu nhân viên
     * @Author nmquang 27-1-2021
     */
    CallApiDataEmployee() {
      var me = this;
      me.Loading = true;
      try {
        axios.get(`${Resource.AMIS_SERVICE_URL}/Employee`).then((res) => {
          me.dataEmployees = res.data.data;
          console.log(me.dataEmployees);
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
     * Hàm xử lý khi bấm lọc
     * @Author nmquang 27-1-2021
     */
    handleBtnFiler() {
      let me = this;
      let temp = [];

      if (me.mAccountObjectGroupCode) {
        temp.push({
          id: me.mAccountObjectGroupCode,
          name: me.mAccountObjectGroupCode,
          type: "mAccountObjectGroupCode",
        });
      }
      if (me.mProvinceOrCity) {
        me.provinceOrCityList.filter(function (value) {
          if (value.location_id == me.mProvinceOrCity) {
            temp.push({
              name: value.location_name,
              type: "mProvinceOrCity",
            });
          }
        });
      }
      if (me.mDistrict) {
        me.districtList.filter(function (value) {
          if (value.location_id == me.mDistrict) {
            temp.push({
              name: value.location_name,
              type: "mDistrict",
            });
          }
        });
      }
      if (me.mWardOrCommune) {
        me.wardOrCommuneList.filter(function (value) {
          if (value.location_id == me.mWardOrCommune) {
            temp.push({
              name: value.location_name,
              type: "mWardOrCommune",
            });
          }
        });
      }
      me.filterArray = temp;
      if (me.filterArray != []) {
        me.filterArray.push({
          name: "Xóa tất cả",
          type: "deleteAll",
        });
      }

      this.isShow = false;
      this.CallApiDataPaging();
    },
    /**
     * Hàm xóa dữ liệu lọc
     * @Author nmquang 27-1-2021
     */
    deleteItemFilter(index, obj) {
      let me = this;
      if (obj.type == "deleteAll") {
        me.mAccountObjectGroupCode = null;
        me.mProvinceOrCity = null;
        me.mDistrict = null;
        me.mWardOrCommune = null;
        me.filterArray = [];
      } else {
        me[obj.type] = null;
        me.filterArray.splice(index, 1);
        if (me.filterArray.length == 1) {
          me.filterArray = [];
        }
      }
      me.CallApiDataPaging();
    },
    /**
     * Hàm làm mới dữ liệu lọc
     * @Author nmquang 27-1-2021
     */
    refreshFilter() {
      let me = this;
      me.mAccountObjectGroupCode = null;
      me.mProvinceOrCity = null;
      me.mDistrict = null;
      me.mWardOrCommune = null;
      me.filterArray = [];
      me.isShow = false;
      me.CallApiDataPaging();
    },
    /**
     * Hàm đóng popup
     * @Author nmquang 27-1-2021
     */
    handleClosePopup() {
      this.showPopup = false;
    },
    /**
     * Hàm xử lý sự kiện khi bấm nút thêm mới
     * @Author nmquang 27-1-2021
     */
    handleBtnAdd() {
      this.showPopup = true;
      this.FormMode = Enum.FormMode.Add;
    },
    /**
     * Hàm xử lý sự kiện khi làm mới dữ liệu
     * @Author nmquang 27-1-2021
     */
    handleBtnRefresh() {
      this.CallApiDataPaging();
    },
    /**
     * Hàm xử lý exporExcel
     * @Author nmquang 19-12-2021
     */
    async exportExcel(event) {
      event.target.parentElement.href = `${Resource.AMIS_SERVICE_URL}/AccountObject/ExportExcel`;
      event.target.parentElement.click();
    },
    /**
     * Hàm xử lý sự kiện khi thay đổi số lượng bản ghi
     * @Author nmquang 27-1-2021
     */
    changeRecordNumber(pageRecord) {
      var me = this;
      me.RecordNumber = pageRecord;
      me.CallApiDataPaging();
    },
    /**
     * Hàm xử lý sự kiện khi thay đổi số trang
     * @Author nmquang 27-1-2021
     */
    handleSelectPanigation(page) {
      var me = this;
      me.PageNumber = page;
      this.CallApiDataPaging();
    },
    /**
     * Hàm xử lý sự kiện khi bấm nhân bản
     * @Author nmquang 27-1-2021
     */
    handleCloneBtnClick() {
      this.showPopup = true;
      this.FormMode = Enum.FormMode.Clone;
    },
    /**
     * Hàm xử lý sự kiện khi sửa
     * @Author nmquang 27-1-2021
     */
    handleEditBtnClick() {
      this.showPopup = true;
      this.FormMode = Enum.FormMode.Edit;
    },
    /**
     * Hàm xử lý sự kiện khi bấm xem
     * @Author nmquang 27-1-2021
     */
    handleShowBtnClick() {
      this.showPopup = true;
      this.FormMode = Enum.FormMode.Show;
    },
    /**
     * Hàm xử lý sự kiện khi bấm cất và thêm
     * @Author nmquang 27-1-2021
     */
    saveAndAddMode() {
      this.handleClosePopup(),
        setTimeout(() => {
          this.handleBtnAdd();
        }, 0);
    },
    /**
     * Hàm xử lý sự kiện khi bấm nút xóa
     * @Author nmquang 27-1-2021
     */
    handleTableDelete(accountObject) {
      var me = this;
      me.accountObjectIdDelete = accountObject.accountObjectId;
      me.accountObjectCodeDelete = accountObject.accountObjectCode;
      me.showMsg = true;
      me.MesageBoxText = Resource["VN"].DeleteAccountObjecConfirm(
        me.accountObjectCodeDelete
      );
      me.MessageBoxType = "warning";
    },
    /**
     * Hàm xử lý sự kiện khi bấm út xóa nhiều
     * @Author nmquang 27-1-2021
     */
    DeleteAll() {
      var me = this;
      var lenght = me.ListAccountObjectDelete.length;
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
     * Hàm xử lý sự kiện khi ấn xác nhận MessageBox
     * @Author nmquang 19-12-2021
     */
    async handleConfirmMessage() {
      var me = this;
      me.showMsg = false;
      try {
        if (me.deletes) {
          var listId = "";
          me.ListAccountObjectDelete.forEach((item) => {
            listId += item + ",";
          });
          listId = listId.slice(0, listId.length - 1);
          me.ListAccountObjectDelete = [];
          await me.CallApiDeleteAll(listId);
        } else {
          await me.callApiDelete(me.accountObjectIdDelete);
        }
        me.deletes = false;

        me.CallApiDataPaging();
      } catch (error) {
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },

    /**
     * Hàm gọi Api xóa 1 đối tượng
     * @Author nmquang 19-12-2021
     */
    async callApiDelete(id) {
      try {
        await axios.delete(`${Resource.AMIS_SERVICE_URL}/AccountObject/${id}`);
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.DeleteSuccess
        );
      } catch (error) {
        this.MessageBoxType = "error";
        this.showMsg = true;
        this.MesageBoxText = Resource["VN"].Error.AccountObjectCodeIsUsed;
      }
    },
    /**
     * Hàm gọi Api xóa nhiều đối tượng
     * @Author nmquang 19-12-2021
     */
    async CallApiDeleteAll(listId) {
      try {
        //let deleteAll = true;
        //nếu chọn xóa tất cả thì listId = 0000-0000-0000-000-0000
        await axios.delete(
          `${Resource.AMIS_SERVICE_URL}/AccountObject/DeleteMulti/${listId}`
        );
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.DeleteSuccess
        );
      } catch (error) {
        this.MessageBoxType = "error";
        this.showMsg = true;
        this.MesageBoxText = Resource["VN"].Error.AccountObjectCodeIsUsed;
        new Toast(
          document.getElementById("toast"),
          Resource["VN"].ToastMessage.Error,
          "error"
        );
      }
    },
    /**
     * Hàm thêm id vào danh sách xóa
     * @Author nmquang 19-12-2021
     */
    insertEmployeeIdToListId(accountObjectId) {
      console.log(accountObjectId);
      var me = this;
      var checkDuplicate = false;
      me.ListAccountObjectDelete.filter(function (value) {
        if (value == accountObjectId) {
          checkDuplicate = true;
        }
      });
      if (!checkDuplicate) {
        me.ListAccountObjectDelete.push(accountObjectId);
      }
      console.log(me.ListAccountObjectDelete);
    },
    /**
     * Hàm xóa id khỏi danh sách xóa
     * @Author nmquang 19-12-2021
     */
    deleteEmployeeIdToListId(accountObjectId) {
      var me = this;
      me.ListAccountObjectDelete = me.ListAccountObjectDelete.filter(function (
        value
      ) {
        // duyệt qua các phần tử của mảng lấy ra phần tử giống tham số truyền vào
        return value != accountObjectId;
      });
      console.log(me.ListAccountObjectDelete);
    },
  },
};
</script>

<style scoped>
@import url("../../assets/styles/components/toast/toast.css");

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
  width: 100%;
}
.m-employee-view .m-employee-view__header .m-employee-view__tittle {
  color: #111;
  font-family: Notosans-Regular;
  font-size: 24px;
  font-weight: 700;
  line-height: normal;
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
.w-1\/3 {
  width: 33.33%;
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
.pd-t-8 {
  padding-top: 8px;
}
.px-2 {
  margin-left: 0.5rem !important;
  margin-right: 0.5rem !important;
}
.pt-3 {
  padding-top: 0.75rem !important;
}
.overview {
  height: 72px;
  margin-left: 20px;
  margin-right: 30px;
  display: flex;
  color: #ffffff;
}

.over-view {
  box-sizing: border-box;
  height: 100%;
}
.p-relative {
  position: relative;
}
.dued-debit-overview {
  padding: 6px 10px;
  height: 100%;
}
.overview .num {
  font-size: 24px;
  height: 33.75px;
  display: flex;
  align-items: center;
}
.overview .m-num-text {
  font-size: 14px;
  font-weight: 500;
  position: absolute;
  display: flex;
  align-items: center;
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
  margin-left: 6px;
}
.m-popup-footer-btn .btn_add {
  background: #2ca01c;
  color: #fff;
}
.m-input-error {
  border: 1px solid red !important;
}
.loading {
  position: fixed;
  background: rgba(255, 255, 255, 0.2);
  top: 0px;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 155;
}

.loading__item {
  position: inherit;
  top: 222px;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}
.loading__icon {
  background: transparent url("../../assets/loading.svg") no-repeat center;
  width: 50px;
  height: 50px;
  transition: all 0.3s ease;
  animation: rotate 1s ease infinite;
}
.overdue:hover {
  padding-top: 0;
  border-bottom: 5px solid red;
}
.total-liabilities:hover {
  padding-top: 0;
  border-bottom: 5px solid red;
}
.rotate {
  transform: rotateX(3.142rad);
}
.droptool {
  position: relative;
}
.droptool .drop-box {
  width: 105px;
  margin: 0 12px;
}
.droptool .drop-filter {
  height: 294px;
  width: 540px;
  left: 24px;
  top: 291px;
  position: fixed;
  z-index: 99999999999999;
  background: #fff;
  padding: 2px 1px;
  border-radius: 2px;
  border: 1px solid #babec5;
}
.padding-body-filter {
  padding: 16px 20px 20px;
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
