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
            style="position: relative"
          >
            <div>
              {{ field.text }}
              <div class="filter icon-16 mi-header-option"></div>
            </div>
          </th>

          <th style="text-align: center">Chức năng</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(originalData, index) in data"
          :ref="originalData[id]"
          :key="index"
          @dblclick="handleEditButtonClick(originalData[id])"
        >
          <td style="width: 40px">
            <div @change="btnSeclectedDelete(originalData[id], $event)">
              <Checkbox :checked="checkAll" />
            </div>
          </td>
          <td
            v-for="(field, index) in fields"
            :key="index"
            :style="field.type === 'date' ? 'text-align:center' : ''"
          >
            <p v-if="field.type === 'date'">
              {{ originalData[field.name] | DateFilter }}
            </p>
            <p v-else-if="field.type === 'gender'">
              {{ originalData[field.name] | GenderFilter }}
            </p>
            <p v-else-if="field.type === 'monney'" style="text-align: right">
              {{ originalData[field.name] | formatMoney }}
            </p>
            <p v-else>{{ originalData[field.name] }}</p>
          </td>
          <td style="width: 200px">
            <div class="flex-center">
              <span
                ref="click"
                class="m-btn-edit"
                @click="handleEditButtonClick(originalData[id])"
                >{{ btnText }}</span
              >
              <DropdownOption
                :parent="type"
                @modify="handleEditButtonClick(originalData[id])"
                @show="handleShowButtonClick(originalData[id])"
                @delete="handleDeleteButtonClick(originalData)"
                @clone="handleCloneBtnClick(originalData[id])"
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
    btnText: String,
    value: String,
    type: String,
    id: String,
  },
  watch: {
    value() {
      console.log(this.value);
    },
  },
  methods: {
    chooseAll() {
      let me = this;
      me.checkAll = !me.checkAll;
      var employees = me.data;
      employees.forEach((emp) => {
        this.btnSeclectedAllDelete(emp[this.id]);
      });
    },
    btnSeclectedAllDelete: function (id) {
      var me = this;
      var trSelected = me.$refs[id];
      if (me.checkAll) {
        trSelected[0].classList.add("selected-Record-Delete");
        me.$emit("insertEmployeeIdToListId", id);
      } else {
        trSelected[0].classList.remove("selected-Record-Delete");
        me.$emit("deleteEmployeeIdToListId", id);
      }
    },
    btnSeclectedDelete: function (id, event) {
      var me = this;
      try {
        var checkbox = event.target;
        var trSelected = me.$refs[id];
        if (checkbox.checked) {
          trSelected[0].classList.add("selected-Record-Delete");
          me.$emit("insertEmployeeIdToListId", id);
        } else {
          trSelected[0].classList.remove("selected-Record-Delete");
          me.$emit("deleteEmployeeIdToListId", id);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm hứng originalDataId khi tạo sự kiện EditoriginalData
     * @Author nmquang 19-12-2021
     */
    handleEditButtonClick(id) {
      this.updateValue(id);
      this.$emit("handleEditButtonClick", id);
    },
    /**
     * Hàm hứng originalDataId tạo sự kiện deleteoriginalData
     * @Author nmquang 19-12-2021
     */
    handleDeleteButtonClick(originalData) {
      this.$emit("handleDeleteButtonClick", originalData);
    },
    /**
     * Hàm hứng originalDataId khi tạo sự kiện cloneoriginalData
     * @Author nmquang 19-12-2021
     */
    handleCloneBtnClick(id) {
      this.updateValue(id);
      this.$emit("handleCloneBtnClick", id);
    },
    handleShowButtonClick(id) {
      this.updateValue(id);
      this.$emit("handleShowButtonClick", id);
    },

    updateValue(value) {
      this.$emit("input", value);
      console.log(value);
    },
  },

  filters: {
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
.mi-18 {
  width: 18px;
  height: 18px;
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
.m-table table tr th .filter {
  position: absolute;
  right: 4px;
  top: 9px;
  width: 16px;
  height: 16px;
  display: none;
}
.m-table table tr th:hover {
   cursor: pointer;
}

.m-table table tr td + td {
  border-left: 1px dotted #c7c7c7;
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