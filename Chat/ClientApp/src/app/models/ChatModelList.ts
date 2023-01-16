import { ChatModel } from "./ChatModel";

export interface ChatModelList {
    chatList: ChatModel[];
    lastMessageText: string;
}