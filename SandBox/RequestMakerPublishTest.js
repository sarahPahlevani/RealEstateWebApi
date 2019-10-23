const gate = require("axios");

const baseUrl = "http://localhost:5001/api";
const args = process.argv.slice(2);

const requestTypeId = args[0] ? Number(args[0]) : 1;
const userId = args[1] ? Number(args[1]) : 14;

// 15	NULL	NULL	agent
// 16	NULL	NULL	RealEstateAdministrator
// 17	NULL	NULL	MarketAssistance
// 18	NULL	NULL	MarketAssistancePlus
// 19	NULL	NULL	AppClient
// 20	NULL	NULL	PropertyOwner

(async () => {
    try {

        let data = {
            requestTypeId: requestTypeId,
            UserAccountIdRequester: userId,
            title: "title",
            description: "description"
        };
        console.log(data);
        console.log("*****************************************");
        const res = await gate.post(`${baseUrl}/request/CreateRequestSandBox`, data);

        console.log(res);
    } catch (error) {
        console.error(error);
    }
})();
