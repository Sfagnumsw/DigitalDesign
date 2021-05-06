export interface CustomAllowanceData {

    dailyAllowance: number
}


import { State } from "@docsvision/webclient/BackOffice/State";
import { GenModels } from "@docsvision/webclient/Generated/DocsVision.WebClient.Models";

export interface CustomChangeStateData {

    needState: string
}


import { GenModels } from "@docsvision/webclient/Generated/DocsVision.WebClient.Models";

export interface CustomEmployeeData {
    phone: string,
    manager: GenModels.EmployeeDataModel
}


import { GenModels } from "@docsvision/webclient/Generated/DocsVision.WebClient.Models";

export interface CustomGroupData {
    groupEmployees: GenModels.EmployeeDataModel[]
}


export interface CustomTicketData {
    totalValue: number
}
