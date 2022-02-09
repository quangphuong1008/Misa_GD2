<template>
  <div class="m-employee-view" style="max-height: calc(100vh - 108px)">
    <div v-if="Loading" class="loading">
      <div class="loading__icon"></div>
    </div>
    <div class="grid__row" style="padding-bottom: 0">
      <div class="m-employee-view__header" style="height: 80px">
        <div>
          <div class="m-employee-view__tittle">Danh sách phiếu chi</div>
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
            <div
              class="drop-filter"
              style="width: 456px; left: 52px"
              v-if="isShow"
            >
              <div class="padding-body-filter">
                <div class="grid__row" style="padding-bottom: 10px">
                  <Combobox :label="'Lý do thu chi'" :noneThead="true" />
                </div>
                <div class="grid__row" style="padding-bottom: 10px">
                  <Combobox :label="'Trạng thái ghi sổ'" :noneThead="true" />
                </div>
                <div class="grid__row" style="padding-bottom: 2px">
                  <Combobox
                    class="w-1/3 p-r-6"
                    :label="'Thời gian'"
                    :width="'200px'"
                    :noneThead="true"
                    :datas="dataFilterDate"
                    :fields="[{ name: 'value' }]"
                    v-model="textTime"
                    :textShow="'value'"
                    :id="'id'"
                    ref="textTime"
                  />
                  <div class="m-text-field w-1/3 p-r-6">
                    <div class="m-text-field__label">
                      <div class="m-text-tittle">Từ ngày</div>
                    </div>
                    <input
                      ref="IdentityDate"
                      style="width: 100%"
                      type="date"
                      v-model="startTime"
                    />
                  </div>
                  <div class="m-text-field w-1/3 p-r-6">
                    <div class="m-text-field__label">
                      <div class="m-text-tittle">Đến ngày</div>
                    </div>
                    <input
                      ref="IdentityDate"
                      style="width: 100%"
                      type="date"
                      v-model="endTime"
                    />
                  </div>
                </div>

                <div class="m-popup-footer" style="padding: 0px">
                  <div class="divide"></div>
                  <div class="grid__row" style="margin-bottom: 10px">
                    <div class="w-1/2">
                      <BaseButton
                        class="m-popup-footer-btn"
                        style="color: black; background: #ffffff"
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
        </div>
        <div style="width: 770px;justify-content: flex-start; display: flex">
          <div class="grid__row p-b-0">
            <div v-if="defaultFilter">Từ đầu năm đến hiện tại</div>
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
        <div class="grid__row" style="justify-content: right; padding: 16px 0">
          <TextInput
            :hintText="'Tìm theo mã phiếu chi'"
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
        :type="'CaPayment'"
        :id="'caPaymentId'"
        :data="dataCaPayments"
        :btnText="'Xem'"
        v-model="focusId"
        @handleDeleteButtonClick="handleTableDelete"
        @insertEmployeeIdToListId="insertIdDeleteToListId"
        @deleteEmployeeIdToListId="deleteIdDeleteToListId"
        @handleCloneBtnClick="handleCloneBtnClick"
        @handleEditButtonClick="handleShowBtnClick"
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
    <CAPaymentDetail
      @saveAndAdd="saveAndAddMode"
      v-if="showPopup"
      @handleClosePopup="handleClosePopup"
      :accountObjects="accountObjects"
      :dataEmployees="dataEmployees"
      :formMode="FormMode"
      :focusId="focusId"
      @reLoadData="CallApiDataPaging"
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

import Resource from "../../assets/js/capayment/resource";
import Button from "../../components/ca_payment/ButtonAdd.vue";
import TextInput from "../../components/base/BaseTextInput.vue";
import Table from "../../components/account_object/BaseCustomTable.vue";
import ComboboxPanigaion from "../../components/custom/ComboboxPanigation.vue";
import PageNumberPanigation from "../../components/custom/PageNumberPanigation.vue";
import DropBox from "../../components/custom/DropBox.vue";
import CAPaymentDetail from "./CAPaymentDetail.vue";
import DropTool from "../../components/account_object/DropTool.vue";
import Combobox from "../../components/account_object/ComboboxAccObj.vue";
import BaseButton from "../../components/base/BaseButton.vue";
import Toast from "../../assets/js/toast";
import BaseMessageBox from "../../components/base/BaseMessageBox.vue";
import Enum from "../../assets/js/enum";
export default {
  components: {
    Button,
    TextInput,
    Table,
    ComboboxPanigaion,
    PageNumberPanigation,
    DropBox,
    CAPaymentDetail,
    DropTool,
    Combobox,
    BaseButton,
    BaseMessageBox,
  },
  created() {
    this.CallApiGetDataAccountObject();
    this.CallApiDataEmployee();
    this.CallApiDataPaging();
  },
  data() {
    return {
      focusId: "",

      accountObjects: [],
      dataEmployees: [],
      dataCaPayments: [],
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

      showMsg: false, // show Message
      showMsgCloseForm: false, //
      EmployeeEditId: null,
      // Lưu lại id cần xóa
      codeDelete: null,
      idDelete: null,
      ListCaPaymentIdDelete: [],
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

      defaultFilter: true,
      startTime: "2022-01-01",
      endTime: this.getNewDate(),
      textTime: "1",
      filterArray: [],
      dataFilterDate: [
        {
          id: "1",
          value: "Đầu năm đến hiện tại",
        },
        {
          id: "2",
          value: "Sáu tháng đầu năm",
        },
        {
          id: "3",
          value: "Sáu tháng cuối năm",
        },
        {
          id: "4",
          value: "Tháng 1",
        },
        {
          id: "5",
          value: "Tháng 2",
        },
        {
          id: "6",
          value: "Quý 1",
        },
        {
          id: "7",
          value: "Hôm qua",
        },
        {
          id: "8",
          value: "Tuần trước",
        },
        {
          id: "9",
          value: "Tháng trước",
        },
        {
          id: "10",
          value: "Quý trước",
        },
        {
          id: "11",
          value: "Năm trước",
        },
        {
          id: "12",
          value: "Đầu tháng đến hiện tại",
        },
        {
          id: "13",
          value: "Đầu tuần đến hiện tại",
        },
      ],
      fields: [
        {
          text: "Ngày hạch toán",
          name: "postedDate",
          type: "date",
        },
        {
          text: "ngày chứng từ",
          name: "caPaymentDate",
          type: "date",
          width: "200px",
        },
        {
          text: "số chứng từ",
          name: "caPaymentNo",
        },
        {
          text: "diễn giải",
          name: "resion",
        },
        {
          text: "số tiền",
          name: "totalAmount",
          type: "monney",
        },
        {
          text: "đối tượng",
          name: "accountObjectName",
        },
        {
          text: "mã đối tượng",
          name: "accountObjectCode",
        },
      ],
    };
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
    filterArray(value){
      if(value.length){
        
        this.defaultFilter = false
      }else{
        this.textTime = "1"
        this.defaultFilter = true
      }

    },
    /**
     * Hàm xử lý sự kiện khi dữ liệu lọc theo thời gian thay đổi
     * @Author nmquang 27-1-2021
     */
    textTime(value) {
      let me = this;
      switch (value) {
        case "1": {
          // dau nam den hien tai
          let today = new Date();
          me.startTime = `${today.getFullYear()}-01-01`;
          me.endTime = me.getNewDate();
          break;
        }

        case "2": {
          // sáu tháng đầu năm
          let today = new Date();

          me.startTime = `${today.getFullYear()}-01-01`;
          me.endTime = `${today.getFullYear()}-05-31`;
          break;
        }
        case "3": {
          // Sáu tháng cuối năm"
          let today = new Date();
          me.startTime = `${today.getFullYear()}-06-01`;
          me.endTime = `${today.getFullYear()}-12-31`;
          break;
        }

        case "4": {
          // tháng 1
          let today = new Date();
          me.startTime = `${today.getFullYear()}-01-01`;
          me.endTime = `${today.getFullYear()}-01-31`;
          break;
        }
        case "5": {
          // tháng 2
          let today = new Date();
          me.startTime = `${today.getFullYear()}-02-01`;
          let isLeap = false;
          let year = today.getFullYear();
          if (year % 4 == 0) {
            //chia hết cho 4 là năm nhuận
            if (year % 100 == 0) {
              //nếu vừa chia hết cho 4 mà vừa chia hết cho 100 thì không phải năm nhuận
              if (year % 400 == 0)
                //chia hết cho 400 là năm nhuận
                isLeap = true;
              else isLeap = false; //không chia hết cho 400 thì không phải năm nhuận
            } //chia hết cho 4 nhưng không chia hết cho 100 là năm nhuận
            else isLeap = true;
          } else {
            isLeap = false;
          }
          if (!isLeap) {
            me.endTime = `${today.getFullYear()}-02-28`;
          } else {
            me.endTime = `${today.getFullYear()}-02-29`;
          }

          break;
        }
        case "6": {
          // quý 1
          let today = new Date();
          me.startTime = `${today.getFullYear()}-01-01`;
          me.endTime = `${today.getFullYear()}-03-31`;
          break;
        }
        case "7": {
          // hôm qua
          var tomorow = new Date(new Date().getTime() - 24 * 60 * 60 * 1000);
          me.startTime = this.convertDate(tomorow);
          me.endTime = this.convertDate(tomorow);
          break;
        }

        case "8": {
          // tuần trước
          var lastWeek = new Date(
            new Date().getTime() - 7 * 24 * 60 * 60 * 1000
          );
          me.startTime = this.convertDate(lastWeek);
          me.endTime = this.getNewDate();
          break;
        }

        case "9": {
          // tháng trước
          let today = me.getNewDate();
          me.startTime = this.caculatorDate(today, 0, 1, 0);
          me.endTime = today;
          break;
        }
        case "10": {
          // năm trước
          let today = new Date();
          let month = today.getMonth();
          switch (month) {
            case 0:
            case 1:
            case 2: {
              me.startTime = `${today.getFullYear() - 1}-10-01`;
              me.endTime = `${today.getFullYear() - 1}-12-31`;
              break;
            }
            case 3:
            case 4:
            case 5: {
              me.startTime = `${today.getFullYear()}-01-01`;
              me.endTime = `${today.getFullYear()}-03-31`;
              break;
            }
            case 6:
            case 7:
            case 8: {
              me.startTime = `${today.getFullYear()}-04-01`;
              me.endTime = `${today.getFullYear()}-06-30`;
              break;
            }
            case 9:
            case 10:
            case 11: {
              me.startTime = `${today.getFullYear()}-07-01`;
              me.endTime = `${today.getFullYear()}-09-30`;
              break;
            }
          }

          break;
        }
        case "11": {
          // năm trước
          let today = me.getNewDate();
          me.startTime = this.caculatorDate(today, 0, 0, 1);
          me.endTime = me.getNewDate();
          break;
        }
        case "12": {
          // đầu tháng đến hiện tại
          let today = new Date();
          let firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
          me.startTime = me.convertDate(firstDay);
          me.endTime = me.getNewDate();

          break;
        }

        case "13": {
          // đầu tuần đến hiện tại
          let today = new Date();
          let monday = me.getMonday(today);
          me.startTime = me.convertDate(monday);
          me.endTime = me.getNewDate();
          break;
        }

       
      }
    },
    /**
     * Hàm xử lý sự kiện khi dữ liệu tìm kiếm thay đổi
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
  },
  methods: {
    /**
     * Hàm xử lý sự kiện cất va thêm
     * @Author nmquang 27-1-2021
     */
    saveAndAddMode() {
      this.handleClosePopup(),
        setTimeout(() => {
          this.handleBtnAdd();
        }, 0);
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
    async CallApiGetDataAccountObject() {
      var me = this;
      try {
        axios.get(`${Resource.AMIS_SERVICE_URL}/AccountObject`).then((res) => {
          me.accountObjects = res.data.data;
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
     * Hàm gọi api lấy dữ liệu tìm kiếm + paging
     * @Author nmquang 27-1-2021
     */
    CallApiDataPaging() {
      var me = this;

      let dataFilter = {
        //  filter : filter,
        // startTime: '2022-01-24',
        // endTime:  '2022-01-26',
        startTime: this.convertDate(me.startTime),
        endTime: this.convertDate(me.endTime),
        searchText: me.SearchText,
        pageSize: me.RecordNumber,
        pageIndex: me.PageNumber,
      };
      me.Loading = true;
      try {
        axios
          .post(
            //  `${Resource.AMIS_SERVICE_URL}/AccountObject/Filter?searchText=${me.SearchText}&pageSize=${me.RecordNumber}&PageIndex=${me.PageNumber}`
            `${Resource.AMIS_SERVICE_URL}/CaPayment/filterPage`,
            dataFilter
          )
          .then((res) => {
            me.dataCaPayments = res.data.data;
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
     * Hàm xử lý khi bấm lọc
     * @Author nmquang 27-1-2021
     */
    handleBtnFiler() {
      let me = this;
      let temp = [];
       if (me.textTime) {
        me.dataFilterDate.filter(function (item) {
          if (item.id == me.textTime) {
            temp.push({
              name: item.value,
              type: "textTime",
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
        me.textTime = "1";
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
     * Hàm đóng popup
     * @Author nmquang 27-1-2021
     */
    handleClosePopup() {
      this.showPopup = false;
      console.log("close");
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
      event.target.parentElement.href = `${Resource.AMIS_SERVICE_URL}/CaPayment/ExportExcel`;
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
     * Hàm xử lý sự kiện khi bấm xem
     * @Author nmquang 27-1-2021
     */
    handleShowBtnClick() {
      this.showPopup = true;
      this.FormMode = Enum.FormMode.Show;
    },
    /**
     * Hàm xử lý sự kiện khi bấm nút xóa
     * @Author nmquang 27-1-2021
     */
    handleTableDelete(obj) {
      var me = this;
      me.idDelete = obj.caPaymentId;
      me.codeDelete = obj.caPaymentNo;
      me.showMsg = true;
      me.MesageBoxText = Resource["VN"].DeleteCaPaymentConfirm(me.codeDelete);
      me.MessageBoxType = "warning";
    },
    /**
     * Hàm xử lý sự kiện khi bấm út xóa nhiều
     * @Author nmquang 27-1-2021
     */
    DeleteAll() {
      var me = this;
      var lenght = me.ListCaPaymentIdDelete.length;
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
      try {
        if (me.deletes) {
          var listId = "";
          me.ListCaPaymentIdDelete.forEach((item) => {
            listId += item + ",";
          });
          listId = listId.slice(0, listId.length - 1);
          me.ListCaPaymentIdDelete = [];
          await me.CallApiDeleteAll(listId);
        } else {
          await me.callApiDelete(me.idDelete);
        }
        me.deletes = false;
        me.showMsg = false;
        me.CallApiDataPaging();
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
     * Hàm gọi Api xóa 1 phiếu chi
     * @Author nmquang 19-12-2021
     */
    async callApiDelete(id) {
      try {
        await axios.delete(
          `${Resource.AMIS_SERVICE_URL}/CaPayment/MaserDetail/${id}`
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
     * Hàm gọi Api xóa nhiều phiếu chi
     * @Author nmquang 19-12-2021
     */
    async CallApiDeleteAll(listId) {
      try {
        //let deleteAll = true;
        //nếu chọn xóa tất cả thì listId = 0000-0000-0000-000-0000
        await axios.delete(
          `${Resource.AMIS_SERVICE_URL}/CaPayment/MaserDetail/DeleteMulti/${listId}`
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
     * Hàm thêm id vào danh sách  xóa
     * @Author nmquang 19-12-2021
     */
    insertIdDeleteToListId(id) {
      var me = this;
      var checkDuplicate = false;
      me.ListCaPaymentIdDelete.filter(function (value) {
        if (value == id) {
          checkDuplicate = true;
        }
      });
      if (!checkDuplicate) {
        me.ListCaPaymentIdDelete.push(id);
      }
    },
    /**
     * Hàm xóa id khỏi danh sách xóa
     * @Author nmquang 19-12-2021
     */
    deleteIdDeleteToListId(id) {
      var me = this;
      me.ListCaPaymentIdDelete = me.ListCaPaymentIdDelete.filter(function (
        value
      ) {
        // duyệt qua các phần tử của mảng lấy ra phần tử giống tham số truyền vào
        return value != id;
      });
    },

    /**
     * hàm Lấy ngày hiện tại
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
     * lấy ngày đầu tiên trong tuàn
     * @Author nmquang 19-12-2021
     */
    getMonday(d) {
      d = new Date(d);
      var day = d.getDay(),
        diff = d.getDate() - day + (day == 0 ? -6 : 1); //
      return new Date(d.setDate(diff));
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
     * Hàm tính toán ngày tháng
     * @Author nmquang 19-12-2021
     */
    // Convert Date:
    caculatorDate(dateString, mday, mmonth, myear) {
      var result = null;
      if (dateString) {
        let date = new Date(dateString);
        let day =
          date.getDate() - mday < 10
            ? `0${date.getDate() - mday}`
            : `${date.getDate() - mday}`;
        let month =
          date.getMonth() - mmonth + 1 < 10
            ? `0${date.getMonth() + 1 - mmonth}`
            : `${date.getMonth() + 1}` - mmonth;
        let year = `${date.getFullYear() - myear}`;
        if (month == "00") {
          month = "12";
          year -= 1;
        }
        result = `${year}-${month}-${day}`;
      }
      return result;
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
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 155;
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
