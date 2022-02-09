<template>
  <div class="m-table">
    <table>
      <thead>
        <tr style="height : 35px !important">
          <th style="width: 40px">
            <div @click="chooseAll()"><Checkbox /></div>
          </th>
          <th
            v-for="(field, index) in fields"
            :key="index"
            :style="[{'text-align' : field.type === 'date'
                ? 'center'
                : ''},
            {width : field.width}
            ]"
          >
            {{ field.text }}
          </th>
          <th style="text-align: center">Chức năng</th>
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
            <p v-else-if="field.type === 'gender'">
              {{ employee[field.name] | GenderFilter }}
            </p>
            <p v-else>{{ employee[field.name] }}</p>
          </td>
          <td style="width:120px">
            <div class="flex-center">
              <span
              
                ref="click"
                @click="handleEditButtonClick(employee.employeeId)"
                class="m-btn-edit"
                >Sửa</span
              >
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
import Checkbox from "./BaseCheckbox.vue";
import Resource from "../../assets/js/resource";
import DropdownOption from "../custom/DropdownOption.vue";
export default {
  components: {
    Checkbox,
    DropdownOption,
  },
  data() {
    return {
      checkAll: false,
    };
  },
  props: {
    fields: Array,
    data: Array,
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