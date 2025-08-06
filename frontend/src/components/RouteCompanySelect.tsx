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
import { useQueryClient } from "@tanstack/react-query";
import type { Company } from "@/types/ResponseTypes";
import type { RouteFormProps } from "./RouteFilters";
import { useSyncQueryParam } from "@/hooks/useSyncQueryParam";

export const RouteCompanySelect = ({ control }: RouteFormProps) => {
    const [companyId, setCompanyId] = useSyncQueryParam("CompanyId", 0);

    const queryClient = useQueryClient();

    const companies: Company[] | undefined = queryClient.getQueryData([
        "companies",
    ]);

    return (
        <FormField
            name="CompanyId"
            control={control}
            render={({ field }) => (
                <FormItem>
                    <FormLabel>Company:</FormLabel>
                    <Select
                        defaultValue={companyId || field.value || " "}
                        onValueChange={(value) => {
                            field.onChange(value);
                            setCompanyId(value);
                        }}
                    >
                        <FormControl>
                            <SelectTrigger className="w-full">
                                <SelectValue placeholder="Company"/>
                            </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                            <SelectItem value={" "}>None</SelectItem>
                            {companies?.map((c) => (
                                <SelectItem key={c.id} value={c.id}>
                                    {c.name}
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
    );
};
