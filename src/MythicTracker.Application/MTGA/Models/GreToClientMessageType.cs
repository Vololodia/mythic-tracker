namespace MythicTracker.Application.MTGA.Models
{
    public enum GreToClientMessageType
    {
        GREMessageType_None = 0,
        GREMessageType_GameStateMessage = 1,
        GREMessageType_ActionsAvailableReq = 2,
        GREMessageType_ChooseStartingPlayerReq = 6,
        GREMessageType_ConnectResp = 7,
        GREMessageType_GetSettingsResp = 9,
        GREMessageType_SetSettingsResp = 10, // 0x0000000A
        GREMessageType_GroupReq = 11, // 0x0000000B
        GREMessageType_IllegalRequest = 12, // 0x0000000C
        GREMessageType_ModalReq = 14, // 0x0000000E
        GREMessageType_MulliganReq = 15, // 0x0000000F
        GREMessageType_OrderReq = 17, // 0x00000011
        GREMessageType_PromptReq = 18, // 0x00000012
        GREMessageType_RevealHandReq = 21, // 0x00000015
        GREMessageType_SelectNReq = 22, // 0x00000016
        GREMessageType_AllowForceDraw = 24, // 0x00000018
        GREMessageType_BinaryGameState = 25, // 0x00000019
        GREMessageType_DeclareAttackersReq = 26, // 0x0000001A
        GREMessageType_SubmitAttackersResp = 27, // 0x0000001B
        GREMessageType_DeclareBlockersReq = 28, // 0x0000001C
        GREMessageType_SubmitBlockersResp = 29, // 0x0000001D
        GREMessageType_AssignDamageReq = 30, // 0x0000001E
        GREMessageType_AssignDamageConfirmation = 31, // 0x0000001F
        GREMessageType_OrderCombatDamageReq = 32, // 0x00000020
        GREMessageType_OrderDamageConfirmation = 33, // 0x00000021
        GREMessageType_SelectTargetsReq = 34, // 0x00000022
        GREMessageType_SubmitTargetsResp = 35, // 0x00000023
        GREMessageType_PayCostsReq = 36, // 0x00000024
        GREMessageType_IntermissionReq = 37, // 0x00000025
        GREMessageType_DieRollResultsResp = 38, // 0x00000026
        GREMessageType_SelectReplacementReq = 39, // 0x00000027
        GREMessageType_SelectNGroupReq = 40, // 0x00000028
        GREMessageType_DistributionReq = 42, // 0x0000002A
        GREMessageType_NumericInputReq = 43, // 0x0000002B
        GREMessageType_SearchReq = 44, // 0x0000002C
        GREMessageType_OptionalActionMessage = 45, // 0x0000002D
        GREMessageType_CastingTimeOptionsReq = 46, // 0x0000002E
        GREMessageType_SelectManaTypeReq = 47, // 0x0000002F
        GREMessageType_SelectFromGroupsReq = 48, // 0x00000030
        GREMessageType_SearchFromGroupsReq = 49, // 0x00000031
        GREMessageType_GatherReq = 50, // 0x00000032
        GREMessageType_QueuedGameStateMessage = 51, // 0x00000033
        GREMessageType_UIMessage = 52, // 0x00000034
        GREMessageType_SubmitDeckReq = 53, // 0x00000035
        GREMessageType_EdictalMessage = 54, // 0x00000036
        GREMessageType_TimeoutMessage = 55, // 0x00000037
        GREMessageType_TimerStateMessage = 56, // 0x00000038
    }
}
