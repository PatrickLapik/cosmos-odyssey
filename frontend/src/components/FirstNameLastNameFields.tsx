import type { Control } from "react-hook-form";

import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Input } from "./ui/input";
import { useState } from "react";
import { useSyncQueryParam } from "@/hooks/useSyncQueryParam";

type FirstLastNameFieldsProps = {
  control: Control<any>;
  saveToParams?: boolean;
};

export const FirstLastNameFields = ({
  control,
  saveToParams = false,
}: FirstLastNameFieldsProps) => {
  const [firstName, setFirstName] = useSyncQueryParam("FirstName");
  const [lastName, setLastName] = useSyncQueryParam("LastName");

  return (
    <>
      <FormField
        control={control}
        name="FirstName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>First name</FormLabel>
            <FormControl>
              <Input
                {...field}
                onChange={(e) => {
                  field.onChange(e);
                  if (!saveToParams) return;
                  setFirstName(e.target.value);
                }}
                placeholder="John"
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      ></FormField>

      <FormField
        control={control}
        name="LastName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Last name</FormLabel>
            <FormControl>
              <Input
                {...field}
                onChange={(e) => {
                  field.onChange(e);
                  if (!saveToParams) return;
                  setLastName(e.target.value);
                }}
                placeholder="Doe"
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      ></FormField>
    </>
  );
};
