<template>
  <div class="m-table">
    <table>
      <thead>
        <tr style="height: 35px !important">
          <th style="width: 40px">
            <div @click="chooseAll()"><Checkbox /></div>
          </th>
          <th
            v-for="(field, index) in fields"
            :key="index"
            :style="[
              { 'text-align': field.type === 'date' ? 'center' : '' },
              { width: field.width },
            ]"
          >
            {{ field.text }}
          </th>
          <th style="text-align: right">Chức năng</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(employee, employeeId) in data"
          :ref="employee.employeeId"
          :key="employeeId"
          @dblclick="handleEditButtonClick(employee.employeeId)"
        >
          <td style="width: 40px">
            <div @change="btnSeclectedDelete(employee.employeeId, $event)">
              <Checkbox :checked="checkAll" />
            </div>
          </td>
          <td
            v-for="(field, index) in fields"
            :key="index"
            :style="field.type === 'date' ? 'text-align:center' : ''"
          >
            <p v-if="field.type === 'date'">
              {{ employee[field.name] | DateFilter }}
            </p>

            <!-- trường hợp khi Sửa -->
            <div v-show="select == index && editable">
              <!-- loại là combobox (enum là 2) -->
              <div v-if="field.inputType == 2">
                <base-combobox></base-combobox>
              </div>

              <!-- loại là ngày tháng  (enum là 1) -->
              <div v-if="field.inputType == 2">
                <input type="date" v-model="employee[field.fieldID]" />
              </div>
            </div>
            <!-- trường hợp khi Xem -->
            <p v-show="select != index">
              {{
                field.customDisplayMethod &&
                typeof field.customDisplayMethod === "function"
                  ? field.customDisplayMethod(employee[field.fieldID])
                  : employee[field.fieldID]
              }}
            </p>
            <!-- loại là ngày tháng  (enum là 1) -->
            <div v-if="field.type == 1">
              <div v-show="select == index">
                <input type="date" v-model="employee[field.fieldID]" />
              </div>
              <p v-show="select != index">{{ employee[field.fieldID] }}</p>
            </div>

            <p v-else-if="field.type === 'gender'">
              {{ employee[field.name] | GenderFilter }}
            </p>
            <p v-else>{{ employee[field.name] }}</p>
          </td>
          <td style="width: 200px">
            <div class="flex-center">
              <span ref="click" class="m-btn-edit">{{ btnText }}</span>
              <DropdownOption
                @delete="handleDeleteButtonClick(employee)"
                @clone="handleCloneBtnClick(employee.employeeId)"
              />
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import Checkbox from "../base/BaseCheckbox.vue";
import Resource from "../../assets/js/resource";
import DropdownOption from "./DropdownOption.vue";
import BaseCombobox from "../base/BaseCombobox.vue";
export default {
  components: {
    Checkbox,
    DropdownOption,
    BaseCombobox,
  },
  data() {
    return {
      checkAll: false,
    };
  },
  props: {
    editable: {
      type: Boolean,
      default: false
    },
    fields: Array,
    /**
     * VD: fields = [
     * {
     *  fieldID: 'employee_name',
     *  fieldText: 'Tên nhân viên',
     *  inputType: 1, (đây là dạng của ô nhập liệu trong trường hợp cho phép sửa kiểu text)
     *  displayType: 1 (đây là kiểu hiển thị khi ở dạng chỉ Xem, phục vụ hiển thị định dạng text, số, căn trái, phải,...)
     *  width: 200,
     *
     * },
     * {
     *  fieldID: 'gender',
     *  fieldText: 'Giới tính',
     *  inputType: 2, (kiểu combobox),
     *  displayType: 1 (kiểu text căn trái)
     *  width: 100,
     *  customDisplayMethod: function(value) {
     *    let result = '';
     *    if (value == 0) {
     *      result = 'Nam';
     *  }
     * else if (value == 1) {
     *      result = 'Nữ';
     *  }
     * else {
     * result = 'Khác';
     * }
     * return result;
     * }
     * },
     * {
     *  fieldID: 'dob',
     *  fieldText: 'Ngày sinh',
     *  inputType: 3, (kiểu input type="date")
     *  displayType: 3 (hiển thị kiểu ngày tháng căn giữa)
     *  width: 200,
     *
     * },
     * {
     *  fieldID: 'amount',
     *  fieldText: 'Số tiền',
     *  inputType: 4, (kiểu input type="number")
     *  displayType: 2 (hiển thị kiểu số tiền căn phải)
     *  width: 200,
     *
     * },
     * ]
     */
    data: Array,
    btnText: String,
  },
  watch: {},
  methods: {
    chooseAll() {
      let me = this;
      me.checkAll = !me.checkAll;
      var employees = me.data;
      employees.forEach((emp) => {
        this.btnSeclectedAllDelete(emp.employeeId);
      });
    },
    btnSeclectedAllDelete: function (employeeId) {
      var me = this;
      var trSelected = me.$refs[employeeId];
      if (me.checkAll) {
        trSelected[0].classList.add("selected-Record-Delete");
        me.$emit("insertEmployeeIdToListId", employeeId);
      } else {
        trSelected[0].classList.remove("selected-Record-Delete");
        me.$emit("deleteEmployeeIdToListId", employeeId);
      }
    },
    btnSeclectedDelete: function (employeeId, event) {
      var me = this;
      try {
        var checkbox = event.target;
        var trSelected = me.$refs[employeeId];
        if (checkbox.checked) {
          trSelected[0].classList.add("selected-Record-Delete");
          me.$emit("insertEmployeeIdToListId", employeeId);
        } else {
          trSelected[0].classList.remove("selected-Record-Delete");
          me.$emit("deleteEmployeeIdToListId", employeeId);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm hứng employeeId khi tạo sự kiện EditEmployee
     * @Author nmquang 19-12-2021
     */
    handleEditButtonClick(id) {
      this.$emit("handleEditButtonClick", id);
    },
    /**
     * Hàm hứng EmployeeId tạo sự kiện deleteEmployee
     * @Author nmquang 19-12-2021
     */
    handleDeleteButtonClick(employee) {
      this.$emit("handleDeleteButtonClick", employee);
    },
    /**
     * Hàm hứng employeeId khi tạo sự kiện cloneEmployee
     * @Author nmquang 19-12-2021
     */
    handleCloneBtnClick(id) {
      this.$emit("handleCloneBtnClick", id);
    },
  },

  filters: {
    // Filter Gender
    GenderFilter(gender) {
      let result = "";
      switch (gender) {
        case "0":
          result = Resource["VN"].Gender.Male;
          break;
        case "1":
          result = Resource["VN"].Gender.Female;
          break;
        case "2":
          result = Resource["VN"].Gender.Other;
          break;
      }
      return result;
    },
    // Filter ngày tháng
    DateFilter(d) {
      var dateString = "";
      if (d == null || d == undefined) {
        dateString = "";
      } else {
        let DateOfBirth = new Date(d);
        let date =
          DateOfBirth.getDate() < 10
            ? `0${DateOfBirth.getDate()}`
            : DateOfBirth.getDate();
        let mont =
          DateOfBirth.getMonth() + 1 < 10
            ? `0${DateOfBirth.getMonth() + 1}`
            : DateOfBirth.getMonth() + 1;
        let year = DateOfBirth.getFullYear();
        dateString = `${date}/${mont}/${year}`;
        dateString = dateString != "NaN/NaN/NaN" ? dateString : "";
      }
      return dateString;
    },
  },
};
</script>

<style scope>
.flex-center {
  display: flex;
  justify-content: center;
  align-items: center;
}

.m-btn-edit {
  font-weight: 600;
  font-size: 13px;
  color: #0075c0;
  cursor: pointer;
  padding-left: 16px;
}
.m-table {
  padding-left: 20px;
  padding-right: 30px;
  box-sizing: border-box;
}

.m-table table {
  border-collapse: collapse;
  width: 100%;
}
.m-table table thead {
  font-size: 12px;
  color: #111;
  text-transform: uppercase;
  background-color: #eceef1;
  position: sticky;
  top: 0;
  z-index: 2;
}
.m-table table tr {
  border-bottom: 1px solid #c7c7c7;
}
.m-table table tbody tr:hover {
  background-color: rgba(0, 120, 100, 0.05);
  cursor: pointer;
}
.m-table table tr th + th {
  border-left: 1px solid #c7c7c7;
}
.m-table table tr td + td {
  border-left: 1px solid #c7c7c7;
}
.m-table table thead tr th {
  height: 34px;
  padding: 0 10px;
  text-align: start;
}
.m-table table tr td {
  padding: 0 10px;
  height: 44px;
}
.m-table tbody {
  color: #111;
}
table tr.selected-Record-Delete {
  /* background-color: #d1edf4; */
  background-color: rgba(26, 164, 200, 0.2);
}
table tr.selected-Record-Delete:hover {
  /* background-color: #d1edf4; */
  background-color: rgba(26, 164, 200, 0.2);
}
</style>