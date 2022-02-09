const Validate = {
    VN: {
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
    }
}


export default Validate;