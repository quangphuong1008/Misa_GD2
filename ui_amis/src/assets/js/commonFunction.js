export const CommonFunction = {

    /**
     * Hàm convert định dạng tiền thành số ( VD : 1.000.000 -> 1000000)
     * CreatedBy : NmQuang (26/10/2021)
    */
    convertMoneyToNumber : function(money){
        if(money)
        {
            return money.replaceAll('.' , '').trim();
        }
        return "";
    },

    /**
     * Hàm định dạng hiển thị tiền tệ
     * @param {any} money :  bất cữ kiểu giữ liệu gì
     * CreatedBy : NmQuang (26/10/2021)
    */
    formatMoney : function(money){
        if (money) {
            return Number(money).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
        }
        return "";
    },

    /**
     * Hàm trả về mã số (VD : Mã số của mã nhân viên)
     * @param {*} employeeCode : Truyền vào mã 
     * @returns : trả về mã số (loại bỏ chữ số)
     * CreatedBy : NmQuang (26/10/2021)
    */

    formatFieldCode : function(fieldCode){
        for(let i = fieldCode.length - 1 ; i >= 0 ; i--)
        {
            if(/[0-9]/.test(fieldCode[i]) == false)
            {
                return Number(fieldCode.slice(i + 1));
            }
        }
        return -1;
    },

    /**
     * Hàm tìm trường có mã số lớn nhất
     * @param {any} res :  danh sách dữ liệu
     * CreatedBy : NmQuang (26/10/2021)
    */

    findMaxFieldCode : function(res , nameFieldCode)
    {
        return Math.max.apply(Math, res.map(function(o) { return CommonFunction.formatFieldCode(o[nameFieldCode])}))
    },

    /**
     * Format dữ liệu ngày tháng sang năm năm/tháng/ngày  -> load lên form chi tiết nhân viên
     * @param {*} date : bất cữ kiểu giữ liệu gì
     * @returns trả về dữ liệu ngày tháng có dạng năm/tháng/ngày  
     * CreatedBy : NmQuang (26/10/2021)
    */
    formatDateToLoadOnForm : function(value){
        if(value)
        {
            let day = new Date(value).getDate();
            let month = new Date(value).getMonth() + 1;
            let year = new Date(value).getFullYear();
            day = day < 10 ? '0' + day : day;
            month = month < 10 ? '0' + month : month;
            return year + '-' + month + '-' + day;
        }
        else{
            return "";
        }
    },

    /**
     * Hàm thực hiện tính độ dài (width) của đoạn text(str)
     * @param {String} str : Text đầu vào
     * @param {Number} fontSize : fontSize đoạn text
     * @returns : độ dài pixel của đoạn text (str)
     * CreatedBy : NmQuang (27/10/2021)
     */
    measureText : function(str, fontSize = 13) {
        if(str){
            const widths = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.2796875,0.2765625,0.3546875,0.5546875,0.5546875,0.8890625,0.665625,0.190625,0.3328125,0.3328125,0.3890625,0.5828125,0.2765625,0.3328125,0.2765625,0.3015625,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.2765625,0.2765625,0.584375,0.5828125,0.584375,0.5546875,1.0140625,0.665625,0.665625,0.721875,0.721875,0.665625,0.609375,0.7765625,0.721875,0.2765625,0.5,0.665625,0.5546875,0.8328125,0.721875,0.7765625,0.665625,0.7765625,0.721875,0.665625,0.609375,0.721875,0.665625,0.94375,0.665625,0.665625,0.609375,0.2765625,0.3546875,0.2765625,0.4765625,0.5546875,0.3328125,0.5546875,0.5546875,0.5,0.5546875,0.5546875,0.2765625,0.5546875,0.5546875,0.221875,0.240625,0.5,0.221875,0.8328125,0.5546875,0.5546875,0.5546875,0.5546875,0.3328125,0.5,0.2765625,0.5546875,0.5,0.721875,0.5,0.5,0.5,0.3546875,0.259375,0.353125,0.5890625]
            const avg = 0.5279276315789471
            return str
            .split('')
            .map(c => c.charCodeAt(0) < widths.length ? widths[c.charCodeAt(0)] : avg)
            .reduce((cur, acc) => acc + cur) * fontSize
        }else{
            return 0
        }
    },

    /**
     * Hàm thực hiện selected vào item của combobox
     * CreatedBy : NmQuang (28/10/2021)
    */
    selectedItemCombobox : function(data){
        var itemCombobox = data.event.target;
        itemCombobox.parentElement.previousSibling.previousElementSibling.value = data.name;

        var cbItems =  itemCombobox.parentElement.children;
        for(const cbitem of cbItems)
        {
            cbitem.classList.remove('m-item-selected');
            var iconRemove = cbitem.children;
            if(iconRemove[0])
            {
                cbitem.removeChild(iconRemove[0]);
            }
        }

        itemCombobox.classList.add("m-item-selected")
        itemCombobox.innerHTML = "<i class='fas fa-check'></i>" + data.name.toString();
        itemCombobox.parentElement.classList.toggle("m-hidden-dropdown")
        itemCombobox.parentElement.previousSibling.innerHTML = "<i class='fas fa-chevron-down'></i>";
    },

    /**
     * Hàm chuyển ký tự có dấu thành ký tự không dấu phục vụ cho việc tìm kiếm
     * @param str : chuỗi đầu vào muốn chuyển sang ký tự không dấu
     * CreatedBy : (28/10/2021)
    */
    removeVietnameseTones :function(str){
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g,"i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");
        str = str.replace(/đ/g,"d");
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        // Some system encode vietnamese combining accent as individual utf-8 characters
        // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
        str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
        // Remove extra spaces
        // Bỏ các khoảng trắng liền nhau
        str = str.replace(/ + /g," ");
        str = str.trim();
        // Remove punctuations
        // Bỏ dấu câu, kí tự đặc biệt
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\\=|\\<|\\>|\?|\/|,|\.|\\:|\\;|\\'|\\"|\\&|\\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g," ");
        return str;
    }
}