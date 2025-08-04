import { useQueryClient } from "@tanstack/react-query";
import {
    Select,
    SelectContent,
    SelectTrigger,
    SelectValue,
    SelectItem,
} from "./ui/select";
import {
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "./ui/form";
import type { Destination } from "@/pages/RoutesPage";
import type { RouteFormProps } from "./RouteFilters";
import { useSyncQueryParam } from "@/hooks/useSyncQueryParam";

export const RouteFromToSelect = ({ control }: RouteFormProps) => {
    const queryClient = useQueryClient();
    const [fromId, setFromId] = useSyncQueryParam("FromId");
    const [toId, setToId] = useSyncQueryParam("ToId");

    const destinations: Destination[] | undefined = queryClient.getQueryData([
        "destinations",
    ]);

    const commonClasses = "w-full";

    return (
        <div className="flex justify-between flex-col xl:flex-row space-x-12">
            {/* from */}
            <FormField
                name="FromId"
                control={control}
                render={({ field }) => (
                    <FormItem className={commonClasses}>
                        <FormLabel>From:</FormLabel>
                        <Select
                            defaultValue={fromId || field.value}
                            onValueChange={(value) => {
                                field.onChange(value);
                                setFromId(value);
                            }}
                        >
                            <FormControl>
                                <SelectTrigger className={commonClasses}>
                                    <SelectValue placeholder="From" />
                                </SelectTrigger>
                            </FormControl>
                            <SelectContent>
                                {destinations?.map((d) => (
                                    <SelectItem key={d.id} value={d.id}>
                                        {d.name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                        <div className="min-h-12">
                            <FormMessage className="text-wrap" />
                        </div>
                    </FormItem>
                )}
            />

            {/* to */}
            <FormField
                name="ToId"
                control={control}
                render={({ field }) => (
                    <FormItem className={commonClasses}>
                        <FormLabel>To:</FormLabel>
                        <Select
                            defaultValue={toId || field.value}
                            onValueChange={(value) => {
                                field.onChange(value);
                                setToId(value);
                            }}
                        >
                            <FormControl>
                                <SelectTrigger className={commonClasses}>
                                    <SelectValue placeholder="To" />
                                </SelectTrigger>
                            </FormControl>
                            <SelectContent>
                                {destinations?.map((d) => (
                                    <SelectItem key={d.id} value={d.id}>
                                        {d.name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                        <div className="min-h-12">
                            <FormMessage className="text-wrap" />
                        </div>
                    </FormItem>
                )}
            />
        </div>
    );
};
