import moment from 'moment';

Date.prototype.toLocaleString = function() {
    return moment(this).format('M/D/YYYY, h:m:s A');
};
